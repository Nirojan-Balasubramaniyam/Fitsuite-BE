using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GYMFeeManagement_System_BE.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITrainingProgramRepository _trainingProgramRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly CloudinaryService _cloudinaryService;

        public MemberService(IMemberRepository memberRepository, CloudinaryService cloudinaryService, ITrainingProgramRepository trainingProgramRepository, IBranchRepository branchRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;
            _trainingProgramRepository = trainingProgramRepository;
            _branchRepository = branchRepository;

        }

        public async Task<PaginatedResponse<MemberResDTO>> GetAllMembers(int pageNumber, int pageSize, bool? isActive, int branchId = 0)
        {
            var memberList = await _memberRepository.GetAllMembers(pageNumber, pageSize, isActive, branchId);

            var allPrograms = await _trainingProgramRepository.GetAllPrograms();

            var memberResDTOList = memberList.Data.Select(member => new MemberResDTO
            {
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                NIC = member.NIC,
                Phone = member.Phone,
                DoB = member.DoB,
                Gender = member.Gender,
                EmergencyContactName = member.EmergencyContactName,
                EmergencyContactNumber = member.EmergencyContactNumber,
                Bmi = member.Bmi,
                ImagePath = member.ImagePath,
                TrainerId = member.TrainerId,
               
                BranchId = member.BranchId,
                Address = member.Address != null ? new AddressResDTO
                {
                    AddressId = member.Address.AddressId,
                    Street = member.Address.Street,
                    City = member.Address.City,
                    District = member.Address.District,
                    Province = member.Address.Province,
                    Country = member.Address.Country
                } : null,
                IsActive = member.IsActive,
                // Calculate total training cost for the member from EnrollPrograms and their TrainingProgram's cost
                MonthlyPayment = member.EnrollPrograms?.Sum(ep =>
                        allPrograms.FirstOrDefault(p => p.ProgramId == ep.ProgramId)?.Cost ?? 0) ?? 0

                 }).ToList();

            return new PaginatedResponse<MemberResDTO>
            {
                TotalRecords = memberList.TotalRecords,
                PageNumber = memberList.PageNumber,
                PageSize = memberList.PageSize,
                Data = memberResDTOList
            };
        }

        public async Task<MemberResDTO> GetMemberById(int memberId)
        {
            var member = await _memberRepository.GetMemberById(memberId);

            var allPrograms = await _trainingProgramRepository.GetAllPrograms();


            var memberRes = new MemberResDTO
            {
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                NIC = member.NIC,
                Phone = member.Phone,
                DoB = member.DoB,
                Gender = member.Gender,
                EmergencyContactName = member.EmergencyContactName,
                EmergencyContactNumber = member.EmergencyContactNumber,
                Bmi = member.Bmi,   
                ImagePath = member.ImagePath,
                TrainerId = member.TrainerId,
                Address = member.Address != null ? new AddressResDTO
                {
                    AddressId = member.Address.AddressId,
                    Street = member.Address.Street,
                    City = member.Address.City,
                    District = member.Address.District,
                    Province = member.Address.Province,
                    Country = member.Address.Country
                } : null,
                IsActive = member.IsActive,
                // Calculate total training cost for the member from EnrollPrograms and their TrainingProgram's cost
                MonthlyPayment = member.EnrollPrograms?.Sum(ep =>
                        allPrograms.FirstOrDefault(p => p.ProgramId == ep.ProgramId)?.Cost ?? 0) ?? 0
            };

            return memberRes;
        }

        public async Task<string> AddMember(MemberReqDTO addMemberReq)
        {
            if (!DateTime.TryParse(addMemberReq.DoB.ToString(), out DateTime dob))
            {
                throw new ArgumentException("Invalid date format for Date of Birth");

            }

            // Validate the email format and uniqueness
            await ValidateEmail(addMemberReq.Email);

            // Validate BranchId exists
            if (addMemberReq.BranchId > 0)
            {
                try
                {
                    await _branchRepository.GetBranchById(addMemberReq.BranchId);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Branch with ID {addMemberReq.BranchId} does not exist. Please provide a valid BranchId.");
                }
            }
            else
            {
                throw new ArgumentException("BranchId is required and must be greater than 0.");
            }

            // Validate and prepare address
            if (addMemberReq.Address == null)
            {
                throw new ArgumentException("Address is required.");
            }

            // Validate required address fields
            if (string.IsNullOrWhiteSpace(addMemberReq.Address.Street))
            {
                throw new ArgumentException("Street is required.");
            }
            if (string.IsNullOrWhiteSpace(addMemberReq.Address.City))
            {
                throw new ArgumentException("City is required.");
            }
            if (string.IsNullOrWhiteSpace(addMemberReq.Address.Country))
            {
                throw new ArgumentException("Country is required.");
            }

            var member = new Member
            {
                FirstName = addMemberReq.FirstName,
                LastName = addMemberReq.LastName,
                Email = addMemberReq.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(addMemberReq.Password),
                NIC = addMemberReq.NIC,
                Phone = addMemberReq.Phone,
                DoB = addMemberReq.DoB,
                Gender = addMemberReq.Gender,
                EmergencyContactName = addMemberReq.EmergencyContactName,
                EmergencyContactNumber = addMemberReq.EmergencyContactNumber,
                TrainerId = addMemberReq.StaffId,
                BranchId = addMemberReq.BranchId,
                
                Address = new Address
                {
                    Street = addMemberReq.Address.Street.Trim(),
                    City = addMemberReq.Address.City.Trim(),
                    // Convert empty strings to null for nullable fields
                    District = string.IsNullOrWhiteSpace(addMemberReq.Address.District) ? null : addMemberReq.Address.District.Trim(),
                    Province = string.IsNullOrWhiteSpace(addMemberReq.Address.Province) ? null : addMemberReq.Address.Province.Trim(),
                    Country = addMemberReq.Address.Country.Trim(),
                },
                IsActive = true
            };

            if (addMemberReq.ImageFile != null)
            {
                member.ImagePath = await UploadImage(addMemberReq.ImageFile);
            }

            var addedMember = await _memberRepository.AddMember(member);

            var token = CreateToken(addedMember);
            return token;

        }



        public async Task<MemberResDTO> UpdateMember(int memberId, MemberReqDTO updateMemberReq)
        {
            var existingMember = await _memberRepository.GetMemberById(memberId);
            if (existingMember == null)
            {
                throw new Exception("Member id is invalid");
            }

            if (updateMemberReq.Email != null)
            {
                await ValidateEmail(updateMemberReq.Email, memberId);
            }

            // Validate BranchId exists if being updated
            if (updateMemberReq.BranchId != 0 && updateMemberReq.BranchId != existingMember.BranchId)
            {
                try
                {
                    await _branchRepository.GetBranchById(updateMemberReq.BranchId);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Branch with ID {updateMemberReq.BranchId} does not exist. Please provide a valid BranchId.");
                }
            }

            existingMember.FirstName = updateMemberReq.FirstName ?? existingMember.FirstName;
            existingMember.LastName = updateMemberReq.LastName ?? existingMember.LastName;
            existingMember.Email = updateMemberReq.Email ?? existingMember.Email;
            existingMember.NIC = updateMemberReq.NIC ?? existingMember.NIC;
            existingMember.Bmi = updateMemberReq.Bmi ?? existingMember.Bmi;
            existingMember.Phone = updateMemberReq.Phone ?? existingMember.Phone;
            existingMember.DoB = updateMemberReq.DoB != default ? updateMemberReq.DoB : existingMember.DoB;
            existingMember.Gender = updateMemberReq.Gender ?? existingMember.Gender;
            existingMember.EmergencyContactName = updateMemberReq.EmergencyContactName ?? existingMember.EmergencyContactName;
            existingMember.EmergencyContactNumber = updateMemberReq.EmergencyContactNumber ?? existingMember.EmergencyContactNumber;
            existingMember.TrainerId = updateMemberReq.StaffId ?? existingMember.TrainerId;
            existingMember.BranchId = updateMemberReq.BranchId != 0 ? updateMemberReq.BranchId : existingMember.BranchId;





            if (updateMemberReq.ImageFile != null)
            {
                existingMember.ImagePath = await UploadImage(updateMemberReq.ImageFile);
            }

            if(updateMemberReq.Password != null && updateMemberReq.Password!="") 
            {
                existingMember.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateMemberReq.Password);
            }

           

            if (updateMemberReq.Address != null)
            {
                if (existingMember.Address == null)
                {
                    existingMember.Address = new Address();
                }
                
                if (!string.IsNullOrWhiteSpace(updateMemberReq.Address.Street))
                {
                    existingMember.Address.Street = updateMemberReq.Address.Street.Trim();
                }
                if (!string.IsNullOrWhiteSpace(updateMemberReq.Address.City))
                {
                    existingMember.Address.City = updateMemberReq.Address.City.Trim();
                }
                if (!string.IsNullOrWhiteSpace(updateMemberReq.Address.Country))
                {
                    existingMember.Address.Country = updateMemberReq.Address.Country.Trim();
                }
                // Convert empty strings to null for nullable fields
                existingMember.Address.District = string.IsNullOrWhiteSpace(updateMemberReq.Address.District) 
                    ? null 
                    : updateMemberReq.Address.District.Trim();
                existingMember.Address.Province = string.IsNullOrWhiteSpace(updateMemberReq.Address.Province) 
                    ? null 
                    : updateMemberReq.Address.Province.Trim();
            }

            var updatedMember = await _memberRepository.UpdateMember(existingMember);


            var updatedMemberRes = new MemberResDTO
            {
                MemberId = updatedMember.MemberId,
                FirstName = updatedMember.FirstName,
                LastName = updatedMember.LastName,
                Email = updatedMember.Email,
                NIC = updatedMember.NIC,
                Phone = updatedMember.Phone,
                DoB = updatedMember.DoB,
                Gender = updatedMember.Gender,
                EmergencyContactName = updatedMember.EmergencyContactName,
                EmergencyContactNumber = updatedMember.EmergencyContactNumber,
                Bmi  = updatedMember.Bmi,
                ImagePath = updatedMember.ImagePath,
                TrainerId = updatedMember.TrainerId,
                Address = updatedMember.Address != null ? new AddressResDTO
                {
                    AddressId = updatedMember.Address.AddressId,
                    Street = updatedMember.Address.Street,
                    City = updatedMember.Address.City,
                    District = updatedMember.Address.District,
                    Province = updatedMember.Address.Province,
                    Country = updatedMember.Address.Country
                } : null,
                IsActive = updatedMember.IsActive
            };

            return updatedMemberRes;
        }

        public async Task<MemberResDTO> UpdateMemberPassword(int memberId, string password)
        {
            var existingMember = await _memberRepository.GetMemberById(memberId);
            if (existingMember == null)
            {
                throw new Exception("Member id is invalid");
            }




            existingMember.PasswordHash = password != null
                ? BCrypt.Net.BCrypt.HashPassword(password)
                : existingMember.PasswordHash;

            var updatedMember = await _memberRepository.UpdateMember(existingMember);


            var updatedMemberRes = new MemberResDTO
            {
                MemberId = updatedMember.MemberId,
                FirstName = updatedMember.FirstName,
                LastName = updatedMember.LastName,
                Email = updatedMember.Email,
                NIC = updatedMember.NIC,
                Phone = updatedMember.Phone,
                DoB = updatedMember.DoB,
                Gender = updatedMember.Gender,
                EmergencyContactName = updatedMember.EmergencyContactName,
                EmergencyContactNumber = updatedMember.EmergencyContactNumber,
                Bmi = updatedMember.Bmi,
                ImagePath = updatedMember.ImagePath,
                TrainerId = updatedMember.TrainerId,
                Address = updatedMember.Address != null ? new AddressResDTO
                {
                    AddressId = updatedMember.Address.AddressId,
                    Street = updatedMember.Address.Street,
                    City = updatedMember.Address.City,
                    District = updatedMember.Address.District,
                    Province = updatedMember.Address.Province,
                    Country = updatedMember.Address.Country
                } : null,
                IsActive = updatedMember.IsActive
            };

            return updatedMemberRes;
        }


        public async Task<string> Login(string email, string password)
        {
            var member = await _memberRepository.GetMemberByEmail(email);
            if (member == null)
            {
                throw new Exception("Member not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, member.PasswordHash))
            {
                throw new Exception("Wrong password.");
            }
            return CreateToken(member);
        }

        public async Task DeleteMember(int memberId)
        {
            await _memberRepository.DeleteMember(memberId);
        }

        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            var profileImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "profileimages");

            if (!Directory.Exists(profileImagesPath))
            {
                Directory.CreateDirectory(profileImagesPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(profileImagesPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/profileimages/{fileName}";
        }

        private async Task ValidateEmail(string email, int? memberId = null)
        {

            var existingMember = await _memberRepository.GetMemberByEmail(email);

            if (existingMember != null)
            {
                // If the memberId is provided and matches the found Member's ID, it means no email conflict
                if (memberId.HasValue && existingMember.MemberId == memberId.Value)
                {
                    return;
                }

                // Otherwise, the email is already taken by another member
                throw new ArgumentException($"{email} is already in use. Please use a different email.");
            }

        }



        private string CreateToken(Member member)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("MemberId", member.MemberId.ToString()));
            claimList.Add(new Claim("Email", member.Email.ToString()));
            claimList.Add(new Claim("NIC", member.NIC.ToString()));
            claimList.Add(new Claim("Phone", member.Phone.ToString()));
            claimList.Add(new Claim("TrainerId", member.TrainerId?.ToString() ?? "null"));
            claimList.Add(new Claim("BranchId", member.BranchId.ToString()));
            claimList.Add(new Claim("Role", "Member"));
            claimList.Add(new Claim("Active", member.IsActive.ToString()));





            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimList,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credintials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file uploaded.");
            }

            // Get the file stream and file name
            using (var stream = file.OpenReadStream())
            {
                // Upload image to Cloudinary and get the URL
                var imageUrl = await _cloudinaryService.UploadImageAsync(stream, file.FileName);

                // Create the image object to store in the database
                var image = new Image
                {
                    Url = imageUrl,
                    FileName = file.FileName,
                    UploadedOn = DateTime.UtcNow
                };

                // Save image URL in the database
                /*  _context.Images.Add(image);
                  await _context.SaveChangesAsync();*/

                // Return the URL of the uploaded image
                return image.Url;
            }
        }
    }
}

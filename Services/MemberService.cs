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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public MemberService(IMemberRepository memberRepository, ITrainingProgramRepository trainingProgramRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _trainingProgramRepository = trainingProgramRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
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
                    Street = addMemberReq.Address.Street,
                    City = addMemberReq.Address.City,
                    District = addMemberReq.Address.District,
                    Province = addMemberReq.Address.Province,
                    Country = addMemberReq.Address.Country,
                },
                IsActive = true
            };

            if (addMemberReq.ImageFile != null)
            {
                member.ImagePath = await SaveImageFileAsync(addMemberReq.ImageFile);
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

            existingMember.FirstName = updateMemberReq.FirstName ?? existingMember.FirstName;
            existingMember.LastName = updateMemberReq.LastName ?? existingMember.LastName;
            existingMember.Email = updateMemberReq.Email ?? existingMember.Email;
            existingMember.NIC = updateMemberReq.NIC ?? existingMember.NIC;
            existingMember.Phone = updateMemberReq.Phone ?? existingMember.Phone;
            existingMember.DoB = updateMemberReq.DoB != default ? updateMemberReq.DoB : existingMember.DoB;
            existingMember.Gender = updateMemberReq.Gender ?? existingMember.Gender;
            existingMember.EmergencyContactName = updateMemberReq.EmergencyContactName ?? existingMember.EmergencyContactName;
            existingMember.EmergencyContactNumber = updateMemberReq.EmergencyContactNumber ?? existingMember.EmergencyContactNumber;
            existingMember.TrainerId = updateMemberReq.StaffId ?? existingMember.TrainerId;
            existingMember.BranchId = updateMemberReq.BranchId != 0 ? updateMemberReq.BranchId : existingMember.BranchId;





            if (updateMemberReq.ImageFile != null)
            {
                existingMember.ImagePath = await SaveImageFileAsync(updateMemberReq.ImageFile);
            }

            existingMember.PasswordHash = updateMemberReq.Password != null
                ? BCrypt.Net.BCrypt.HashPassword(updateMemberReq.Password)
                : existingMember.PasswordHash;

            if (existingMember.Address != null)
            {
                existingMember.Address.Street = updateMemberReq.Address.Street;
                existingMember.Address.City = updateMemberReq.Address.City;
                existingMember.Address.District = updateMemberReq.Address.District;
                existingMember.Address.Province = updateMemberReq.Address.Province;
                existingMember.Address.Country = updateMemberReq.Address.Country;
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
    }
}

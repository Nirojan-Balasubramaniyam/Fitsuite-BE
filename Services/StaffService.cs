using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GYMFeeManagement_System_BE.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly CloudinaryService _cloudinaryService;


        public StaffService(IStaffRepository staffRepository, CloudinaryService cloudinaryService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;

        }

        public async Task<PaginatedResponse<StaffResDTO>> GetAllStaffs(int pageNumber, int pageSize, bool isActive)
        {
            var staffList = await _staffRepository.GetAllStaffs(pageNumber, pageSize, isActive);

            var staffResList = staffList.Data.Select(staff => new StaffResDTO
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                NIC = staff.NIC,
                Phone = staff.Phone,
                DoB = staff.DoB,
                Gender = staff.Gender,
                ImagePath = staff.ImagePath,
                UserRole = staff.UserRole,
                BranchId = staff.BranchId.HasValue ? staff.BranchId.Value : 0,
                Address = staff.Address != null ? new AddressResDTO
                {
                    AddressId = staff.Address.AddressId,
                    Street = staff.Address.Street,
                    City = staff.Address.City,
                    District = staff.Address.District,
                    Province = staff.Address.Province,
                    Country = staff.Address.Country
                } : null
            }).ToList();

            return new PaginatedResponse<StaffResDTO>
            {
                TotalRecords = staffList.TotalRecords,
                PageNumber = staffList.PageNumber,
                PageSize = staffList.PageSize,
                Data = staffResList
            };
        }

        public async Task<StaffResDTO> GetStaffById(int staffId)
        {
            var staff = await _staffRepository.GetStaffById(staffId);


            var staffRes = new StaffResDTO
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                NIC = staff.NIC,
                Phone = staff.Phone,
                DoB = staff.DoB,
                Gender = staff.Gender,
                ImagePath = staff.ImagePath,
                UserRole = staff.UserRole,
                BranchId = staff.BranchId.HasValue ? staff.BranchId.Value : 0 ,
                Address = staff.Address != null ? new AddressResDTO
                {
                    AddressId = staff.Address.AddressId,
                    Street = staff.Address.Street,
                    City = staff.Address.City,
                    District = staff.Address.District,
                    Province = staff.Address.Province,
                    Country = staff.Address.Country
                } : null
            };

            return staffRes;
        }

        public async Task<string> AddStaff(StaffReqDTO addStaffReq)
        {
            if (!DateTime.TryParse(addStaffReq.DoB.ToString(), out DateTime dob))
            {
                throw new ArgumentException("Invalid date format for Date of Birth");
            }

            // Validate the email format and uniqueness
            await ValidateEmail(addStaffReq.Email);

            var staff = new Staff
            {
                FirstName = addStaffReq.FirstName,
                LastName = addStaffReq.LastName,
                Email = addStaffReq.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(addStaffReq.Password),
                NIC = addStaffReq.NIC,
                Phone = addStaffReq.Phone,
                DoB = addStaffReq.DoB,
                Gender = addStaffReq.Gender,  
                UserRole = addStaffReq.UserRole,
                IsActive = true,
                BranchId = addStaffReq.BranchId,
                Address = new Address
                 {
                     Street = addStaffReq.Address.Street,
                     City = addStaffReq.Address.City,
                     District = addStaffReq.Address.District,
                     Province = addStaffReq.Address.Province,
                     Country = addStaffReq.Address.Country,
                 }
            };

            if (addStaffReq.ImageFile != null)
            {
                staff.ImagePath = await UploadImage(addStaffReq.ImageFile);
            }

            var addedStaff = await _staffRepository.AddStaff(staff);

            var token = CreateToken(addedStaff);
            return token;

        }



        public async Task<StaffResDTO> UpdateStaff(int staffId, StaffReqDTO updateStaffReq)
        {
            var existingStaff = await _staffRepository.GetStaffById(staffId);
            if (existingStaff == null)
            {
                throw new Exception("Staff id is invalid");
            }

            if (updateStaffReq.Email != null)
            {
                await ValidateEmail(updateStaffReq.Email, staffId);
            }

            existingStaff.FirstName = updateStaffReq.FirstName ?? existingStaff.FirstName;
            existingStaff.LastName = updateStaffReq.LastName ?? existingStaff.LastName;
            existingStaff.Email = updateStaffReq.Email ?? existingStaff.Email;
            existingStaff.NIC = updateStaffReq.NIC ?? existingStaff.NIC;
            existingStaff.Phone = updateStaffReq.Phone ?? existingStaff.Phone;
            existingStaff.DoB = updateStaffReq.DoB != default ? updateStaffReq.DoB : existingStaff.DoB;
            existingStaff.Gender = updateStaffReq.Gender ?? updateStaffReq.Gender;
            existingStaff.BranchId = updateStaffReq.BranchId != 0 ? updateStaffReq.BranchId : existingStaff.BranchId;
            existingStaff.UserRole = updateStaffReq.UserRole != 0 ? updateStaffReq.UserRole : existingStaff.UserRole;


            if (updateStaffReq.ImageFile != null)
            {
                existingStaff.ImagePath = await UploadImage(updateStaffReq.ImageFile);
            }

           /* existingStaff.PasswordHash = updateStaffReq.Password != null
                ? BCrypt.Net.BCrypt.HashPassword(updateStaffReq.Password)
                : existingStaff.PasswordHash;*/

            if (updateStaffReq.Address != null)
            {
                existingStaff.Address = new Address
                {
                    Street = updateStaffReq.Address.Street,
                    City = updateStaffReq.Address.City,
                    District = updateStaffReq.Address.District,
                    Province = updateStaffReq.Address.Province,
                    Country = updateStaffReq.Address.Country,
                };

            }

            var updatedStaff = await _staffRepository.UpdateStaff(existingStaff);

            var updatedStaffRes = new StaffResDTO
            {
                StaffId = updatedStaff.StaffId,
                FirstName = updatedStaff.FirstName,
                LastName = updatedStaff.LastName,
                Email = updatedStaff.Email,
                NIC = updatedStaff.NIC,
                Phone = updatedStaff.Phone,
                DoB = updatedStaff.DoB,
                Gender = updatedStaff.Gender,
                UserRole = updatedStaff.UserRole,
                BranchId = updatedStaff.BranchId.HasValue ? updatedStaff.BranchId.Value : 0,
                ImagePath = updatedStaff.ImagePath,
                Address = updatedStaff.Address != null ? new AddressResDTO
                {
                    AddressId = updatedStaff.Address.AddressId,
                    Street = updatedStaff.Address.Street,
                    City = updatedStaff.Address.City,
                    District = updatedStaff.Address.District,
                    Province = updatedStaff.Address.Province,
                    Country = updatedStaff.Address.Country
                } : null
            };

            return updatedStaffRes;

        }

        public async Task<Boolean> CheckStaffPassword(int staffId, string password)
        {
            var existingStaff = await _staffRepository.GetStaffById(staffId);
            var isChecked = BCrypt.Net.BCrypt.Verify(password, existingStaff.PasswordHash);
            return isChecked;

        }

        public async Task<Boolean> UpdateStaffPassword(int staffId, string password)
        {
            var existingStaff = await _staffRepository.GetStaffById(staffId);
            if (existingStaff == null)
            {
                throw new Exception("Staff id is invalid");
            }




            existingStaff.PasswordHash = password != null
                ? BCrypt.Net.BCrypt.HashPassword(password)
                : existingStaff.PasswordHash;

            var updatedMember = await _staffRepository.UpdateStaff(existingStaff);
            if(updatedMember == null)
            {
                return false;
            }

            return true;


            
        }


        public async Task<string> Login(string email, string password)
        {
            var staff = await _staffRepository.GetStaffByEmail(email);
            if (staff == null)
            {
                throw new Exception("Staff not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, staff.PasswordHash))
            {
                throw new Exception("Wrong password.");
            }
            return CreateToken(staff);
        }

        public async Task DeleteStaff(int staffId)
        {
            await _staffRepository.DeleteStaff(staffId);
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

        private async Task ValidateEmail(string email, int? staffId = null)
        {

            var existingStaff = await _staffRepository.GetStaffByEmail(email);

            if (existingStaff != null)
            {
                // If the staffId is provided and matches the found Staff's ID, it means no email conflict
                if (staffId.HasValue && existingStaff.StaffId == staffId.Value)
                {
                    return;
                }

                // Otherwise, the email is already taken by another staff
                throw new ArgumentException($"{email} is already in use. Please use a different email.");
            }
        }



        private string CreateToken(Staff staff)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("StaffId", staff.StaffId.ToString()));
            claimList.Add(new Claim("Email", staff.Email.ToString()));
            claimList.Add(new Claim("NIC", staff.NIC.ToString()));
            claimList.Add(new Claim("Phone", staff.Phone.ToString()));
            claimList.Add(new Claim("BranchId", staff.BranchId.ToString()));
            claimList.Add(new Claim("Role", staff.UserRole.ToString()));



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

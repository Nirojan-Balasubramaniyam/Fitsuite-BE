using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IStaffRepository _staffRepository;


        public BranchService(IBranchRepository branchRepository, IStaffRepository staffRepository)
        {
            _branchRepository = branchRepository;
            _staffRepository = staffRepository;
        }

        public async Task<BranchResDTO> GetBranchById(int branchId)
        {
            var branch = await _branchRepository.GetBranchById(branchId);

            var branchResDTO = new BranchResDTO
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                Address = branch.Address != null ? new AddressResDTO
                {
                    AddressId = branch.Address.AddressId,
                    Street = branch.Address.Street,
                    City = branch.Address.City,
                    District = branch.Address.District,
                    Province = branch.Address.Province,
                    Country = branch.Address.Country
                } : null,

                BranchAdminId = branch.Staffs != null ?
                    branch.Staffs
                    .Where(s => s.UserRole == UserRole.admin || s.UserRole == UserRole.superAdmin)
                    .Select(s => s.StaffId)
                    .FirstOrDefault() : 0,
                AdminName = branch.Staffs != null ?
                    branch.Staffs
                        .Where(s => s.UserRole == UserRole.admin || s.UserRole == UserRole.superAdmin)
                        .Select(s => s.FirstName + " " + s.LastName)
                        .FirstOrDefault() ?? ""  
                    : "",
                IsActive = branch.IsActive,
                ActiveMembers = branch.Members?.Count(m => m.IsActive == true) ?? 0,  // Active members
                LeavedMembers = branch.Members?.Count(m => m.IsActive == false) ?? 0
            };

            return branchResDTO;
        }

        public async Task<PaginatedResponse<BranchResDTO>> GetAllBranches(int pageNumber, int pageSize)
        {
            var branchList = await _branchRepository.GetAllBranches(pageNumber, pageSize);

            var branchResDTOList = branchList.Data.Select(branch => new BranchResDTO
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                Address = branch.Address != null ? new AddressResDTO
                {
                    AddressId = branch.Address.AddressId,
                    Street = branch.Address.Street,
                    City = branch.Address.City,
                    District = branch.Address.District,
                    Province = branch.Address.Province,
                    Country = branch.Address.Country
                } : null,
                BranchAdminId = branch.Staffs != null ?
                    branch.Staffs
                    .Where(s => s.UserRole == UserRole.admin || s.UserRole == UserRole.superAdmin)
                    .Select(s => s.StaffId)
                    .FirstOrDefault() : 0,
                AdminName = branch.Staffs != null ?
                    branch.Staffs
                        .Where(s => s.UserRole == UserRole.admin || s.UserRole == UserRole.superAdmin)
                        .Select(s => s.FirstName + " " + s.LastName)
                        .FirstOrDefault() ?? ""
                    : "",
                IsActive = branch.IsActive
            }).ToList();

            return new PaginatedResponse<BranchResDTO>
            {
                TotalRecords = branchList.TotalRecords,
                PageNumber = branchList.PageNumber,
                PageSize = branchList.PageSize,
                Data = branchResDTOList
            };
        }


        public async Task<BranchResDTO> AddBranch(BranchReqDTO branchRequest, int adminStaffId)
        {
            // Validate and prepare address
            if (branchRequest.Address == null)
            {
                throw new ArgumentException("Address is required.");
            }

            // Validate required address fields
            if (string.IsNullOrWhiteSpace(branchRequest.Address.Street))
            {
                throw new ArgumentException("Street is required.");
            }
            if (string.IsNullOrWhiteSpace(branchRequest.Address.City))
            {
                throw new ArgumentException("City is required.");
            }
            if (string.IsNullOrWhiteSpace(branchRequest.Address.Country))
            {
                throw new ArgumentException("Country is required.");
            }

            // Only validate and assign admin if adminStaffId is provided (greater than 0)
            if (adminStaffId > 0)
            {
                var adminAssigned = await _staffRepository.AdminIsAssignedToAnotherBranchAsync(adminStaffId);

                if (adminAssigned)
                {
                    // Admin is already assigned to another branch
                    throw new Exception("The selected Admin already assigned to a Branch!!");
                }
            }

            var branch = new Branch
            {
                BranchName = branchRequest.BranchName,
                Address = new Address
                {
                    Street = branchRequest.Address.Street.Trim(),
                    City = branchRequest.Address.City.Trim(),
                    // Convert empty strings to null for nullable fields
                    District = string.IsNullOrWhiteSpace(branchRequest.Address.District) ? null : branchRequest.Address.District.Trim(),
                    Province = string.IsNullOrWhiteSpace(branchRequest.Address.Province) ? null : branchRequest.Address.Province.Trim(),
                    Country = branchRequest.Address.Country.Trim(),
                },
                IsActive = true
            };
            var addedBranch = await _branchRepository.AddBranch(branch);

            // Only assign admin if adminStaffId is provided (greater than 0)
            if (adminStaffId > 0)
            {
                try
                {
                    var admin = await _staffRepository.GetStaffById(adminStaffId);
                    if (admin != null)
                    {
                        admin.BranchId = addedBranch.BranchId;  // Assign admin to the new branch
                        await _staffRepository.UpdateStaff(admin);
                    }
                }
                catch (Exception)
                {
                    throw new Exception($"Staff with ID {adminStaffId} not found. Please provide a valid BranchAdminId or set it to 0 to create a branch without an admin.");
                }
            }

            var branchResDTO = new BranchResDTO
            {
                BranchId = addedBranch.BranchId,
                BranchName = addedBranch.BranchName,
                BranchAdminId = adminStaffId > 0 ? adminStaffId : 0,
                Address = addedBranch.Address != null ? new AddressResDTO
                {
                    AddressId = addedBranch.Address.AddressId,
                    Street = addedBranch.Address.Street,
                    City = addedBranch.Address.City,
                    District = addedBranch.Address.District,
                    Province = addedBranch.Address.Province,
                    Country = addedBranch.Address.Country
                } : null,
                IsActive = addedBranch.IsActive
            };

            return branchResDTO; ;
        }



        public async Task<BranchResDTO> UpdateBranch(int branchId, BranchReqDTO branchRequest)
        {
            var existingBranch = await _branchRepository.GetBranchById(branchId);
            if (existingBranch == null)
            {
                throw new Exception("Branch id is invalid");
            }
            existingBranch.BranchName = branchRequest.BranchName;
            if (branchRequest.Address != null)
            {
                if (existingBranch.Address == null)
                {
                    existingBranch.Address = new Address();
                }
                
                if (!string.IsNullOrWhiteSpace(branchRequest.Address.Street))
                {
                    existingBranch.Address.Street = branchRequest.Address.Street.Trim();
                }
                if (!string.IsNullOrWhiteSpace(branchRequest.Address.City))
                {
                    existingBranch.Address.City = branchRequest.Address.City.Trim();
                }
                if (!string.IsNullOrWhiteSpace(branchRequest.Address.Country))
                {
                    existingBranch.Address.Country = branchRequest.Address.Country.Trim();
                }
                // Convert empty strings to null for nullable fields
                existingBranch.Address.District = string.IsNullOrWhiteSpace(branchRequest.Address.District) 
                    ? null 
                    : branchRequest.Address.District.Trim();
                existingBranch.Address.Province = string.IsNullOrWhiteSpace(branchRequest.Address.Province) 
                    ? null 
                    : branchRequest.Address.Province.Trim();
            }

            var updatedBranch = await _branchRepository.UpdateBranch(existingBranch);

            var branchResDTO = new BranchResDTO
            {
                BranchId = updatedBranch.BranchId,
                BranchName = updatedBranch.BranchName,
                Address = updatedBranch.Address != null ? new AddressResDTO
                {
                    AddressId = updatedBranch.Address.AddressId,
                    Street = updatedBranch.Address.Street,
                    City = updatedBranch.Address.City,
                    District = updatedBranch.Address.District,
                    Province = updatedBranch.Address.Province,
                    Country = updatedBranch.Address.Country
                } : null
            };

            return branchResDTO; ;
        }

        public async Task DeleteBranch(int branchId)
        {
            await _branchRepository.DeleteBranch(branchId);
        }


    }
}

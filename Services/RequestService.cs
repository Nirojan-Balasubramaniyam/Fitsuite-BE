using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.DTOs.Response.RequestResponseDTOs;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Abstractions;

namespace GYMFeeManagement_System_BE.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly CloudinaryService _cloudinaryService;


        public RequestService(IRequestRepository requestRepository, CloudinaryService cloudinaryService, IMemberRepository memberRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _requestRepository = requestRepository;
            _memberRepository = memberRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<NeworChangeMemberRequestResDTO> AddMemberRequestAsync(NeworChangeMemberRequestDTO requestDTO, DateTime paidDate)
        {

            // Validate the email format and uniqueness
            await ValidateEmail(requestDTO.Email);

            var request = new Request
            {
                RequestType = "NewMember",
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                Email = requestDTO.Email,
                NIC = requestDTO.NIC,
                Phone = requestDTO.Phone,
                DOB = requestDTO.DOB,
                Gender = requestDTO.Gender,
                ReceiptNumber = requestDTO.PaymentReceipt,
                EmergencyContactName = requestDTO.EmergencyContactName,
                EmergencyContactNumber = requestDTO.EmergencyContactNumber,
                Password = requestDTO.Password,
                Address = requestDTO.Address != null ? new Address
                {
                    Street = !string.IsNullOrWhiteSpace(requestDTO.Address.Street) ? requestDTO.Address.Street.Trim() : null,
                    City = !string.IsNullOrWhiteSpace(requestDTO.Address.City) ? requestDTO.Address.City.Trim() : null,
                    // Convert empty strings to null for nullable fields
                    District = string.IsNullOrWhiteSpace(requestDTO.Address.District) ? null : requestDTO.Address.District.Trim(),
                    Province = string.IsNullOrWhiteSpace(requestDTO.Address.Province) ? null : requestDTO.Address.Province.Trim(),
                    Country = !string.IsNullOrWhiteSpace(requestDTO.Address.Country) ? requestDTO.Address.Country.Trim() : null,
                } : null,
                PaidDate = paidDate,
                Status = "pending"
            };
            if (requestDTO.ImageFile != null)
            {
                request.ImagePath = await UploadImage(requestDTO.ImageFile);
            }

            var addedRequest = await _requestRepository.AddRequest(request);

            return new NeworChangeMemberRequestResDTO
            {
                RequestId = addedRequest.RequestId,
                RequestType = addedRequest.RequestType,
                FirstName = addedRequest.FirstName,
                LastName = addedRequest.LastName,
                Email = addedRequest.Email,
                Phone = addedRequest.Phone,
                NIC = addedRequest.NIC,
                DOB = addedRequest.DOB,
                PaymentReceipt = addedRequest.ReceiptNumber,
                Password = addedRequest.Password,   
                Gender = addedRequest.Gender,
                ImagePath = addedRequest.ImagePath,
                Address = addedRequest.Address != null ? new AddressReqDTO
                {
                    Street = addedRequest.Address.Street,
                    City = addedRequest.Address.City,
                    District = addedRequest.Address.District,
                    Province = addedRequest.Address.Province,
                    Country = addedRequest.Address.Country
                } : null,
                EmergencyContactName = addedRequest.EmergencyContactName,
                EmergencyContactNumber = addedRequest.EmergencyContactNumber,
                Status = addedRequest.Status,

            };
        }

        public async Task<NeworChangeMemberRequestResDTO> ChangeMemberInfoRequestAsync(NeworChangeMemberRequestDTO requestDTO)
        {

            // Validate the email format and uniqueness
            await ValidateEmail(requestDTO.Email);

            var request = new Request
            {
                RequestType = "MemberInfo",
                MemberId = requestDTO.MemberId,
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                Email = requestDTO.Email,
                NIC = requestDTO.NIC,
                Phone = requestDTO.Phone,
                DOB = requestDTO.DOB,
                Gender = requestDTO.Gender,
                EmergencyContactName = requestDTO.EmergencyContactName,
                EmergencyContactNumber = requestDTO.EmergencyContactNumber,
                Password = requestDTO.Password,
                Address = requestDTO.Address != null ?  new Address
                {
                    Street = requestDTO.Address.Street,
                    City = requestDTO.Address.City,
                    District = requestDTO.Address.District,
                    Province = requestDTO.Address.Province,
                    Country = requestDTO.Address.Country,
                } : null,
                Status = "pending"
            };
            if (requestDTO.ImageFile != null)
            {
                request.ImagePath = await UploadImage(requestDTO.ImageFile);
            }

            var addedRequest = await _requestRepository.AddRequest(request);

            return new NeworChangeMemberRequestResDTO
            {
                RequestId = addedRequest.RequestId,
                RequestType = addedRequest.RequestType,
                FirstName = addedRequest.FirstName,
                LastName = addedRequest.LastName,
                Email = addedRequest.Email,
                Phone = addedRequest.Phone,
                NIC = addedRequest.NIC,
                DOB = request.DOB,
                Gender = addedRequest.Gender,
                ImagePath = addedRequest.ImagePath,
                Address = addedRequest.Address != null ? new AddressReqDTO
                {
                    Street = addedRequest.Address.Street,
                    City = addedRequest.Address.City,
                    District = addedRequest.Address.District,
                    Province = addedRequest.Address.Province,
                    Country = addedRequest.Address.Country
                } : null,
                EmergencyContactName = addedRequest.EmergencyContactName,
                EmergencyContactNumber = addedRequest.EmergencyContactNumber,
                Status = addedRequest.Status,
            };
        }

        public async Task<ProgramAddonRequestResDTO> AddProgramAddonRequestAsync(ProgramAddonRequestDTO requestDTO)
        {
            var request = new Request
            {
                RequestType = "ProgramAddon",
                MemberId = requestDTO.MemberId,
                Amount = requestDTO.Amount,
                ProgramId = requestDTO.ProgramId,
                Status = "pending"
            };

            var addedRequest = await _requestRepository.AddRequest(request);

            return new ProgramAddonRequestResDTO
            {
                RequestId = addedRequest.RequestId,
                RequestType = addedRequest.RequestType,
                MemberId = addedRequest.MemberId,
                Amount = addedRequest.Amount,
                PaidDate = addedRequest.PaidDate,
                ProgramId = addedRequest.ProgramId,
                ReceiptNumber = addedRequest.ReceiptNumber,
                Status = addedRequest.Status,
            };
        }

        public async Task<LeaveProgramRequestResDTO> AddLeaveProgramRequestAsync(LeaveProgramRequestDTO requestDTO)
        {
            var request = new Request
            {
                RequestType = "LeaveProgram",
                MemberId = requestDTO.MemberId,
                ProgramId = requestDTO.ProgramId,
                Status = "pending"
            };

            var addedRequest = await _requestRepository.AddRequest(request);

            return  new LeaveProgramRequestResDTO
            {
                RequestId = addedRequest.RequestId,
                RequestType = addedRequest.RequestType,
                MemberId = addedRequest.MemberId,
                ProgramId = addedRequest.ProgramId,
                Status = addedRequest.Status,
            };
        }


        public async Task<PaymentRequestResDTO> AddPaymentRequestAsync(PaymentRequestDTO requestDTO)
        {
            var request = new Request
            {
                RequestType = "Payment",
                MemberId = requestDTO.MemberId,
                PaymentType = requestDTO.PaymentType,
                Amount = requestDTO.Amount,
                ReceiptNumber = requestDTO.ReceiptNumber,
                PaidDate = requestDTO.PaidDate,
                DueDate = requestDTO.DueDate,
                Status = "pending"
            };

            var addedRequest = await _requestRepository.AddRequest(request);

            return new PaymentRequestResDTO
            {
                RequestId = addedRequest.RequestId,
                RequestType = addedRequest.RequestType,
                MemberId = addedRequest.MemberId,
                PaymentType = addedRequest.PaymentType,
                Amount = addedRequest.Amount,
                ReceiptNumber = addedRequest.ReceiptNumber,
                PaidDate = addedRequest.PaidDate,
                DueDate = addedRequest.DueDate,
                Status = addedRequest.Status,
            };
        }

        public async Task<PaginatedResponse<ProgramAddonRequestResDTO>> GetProgramAddonRequestsAsync( int pageNumber, int pageSize)
        {
         

            var requestList = await _requestRepository.GetRequestByType("ProgramAddon", pageNumber, pageSize);

            var requestDTOList = requestList.Data.Select(request => new ProgramAddonRequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                MemberId = request.MemberId,
                Amount = request.Amount,
                PaidDate = request.PaidDate,
                ProgramId = request.ProgramId,
                ReceiptNumber = request.ReceiptNumber,
                Status = request.Status,
            }).ToList();


            return new PaginatedResponse<ProgramAddonRequestResDTO>
            {
                TotalRecords = requestList.TotalRecords,
                PageNumber = requestList.PageNumber,
                PageSize = requestList.PageSize,
                Data = requestDTOList
            };
        }

        public async Task<PaginatedResponse<LeaveProgramRequestResDTO>> GetLeaveProgramRequestsAsync( int pageNumber, int pageSize)
        {  

            var requestList = await _requestRepository.GetRequestByType("LeaveProgram", pageNumber, pageSize);

            var requestDTOList = requestList.Data.Select(request => new LeaveProgramRequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                MemberId = request.MemberId,
                ProgramId = request.ProgramId,
                Status = request.Status,
            }).ToList();

            return new PaginatedResponse<LeaveProgramRequestResDTO>
            {
                TotalRecords = requestList.TotalRecords,
                PageNumber = requestList.PageNumber,
                PageSize = requestList.PageSize,
                Data = requestDTOList
            };
        }

        public async Task<PaginatedResponse<NeworChangeMemberRequestResDTO>> GetNewMemberRequestsAsync( int pageNumber, int pageSize)
        {
          
            var requestList = await _requestRepository.GetRequestByType("NewMember", pageNumber, pageSize);

            var requestDTOList = requestList.Data.Select(request => new NeworChangeMemberRequestResDTO
            {
                RequestId= request.RequestId,
                RequestType = request.RequestType,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                NIC = request.NIC,
                DOB = request.DOB,
                Gender = request.Gender,
                PaymentReceipt = request.ReceiptNumber,
                ImagePath = request.ImagePath,
                Password = request.Password,
                Address = request.Address !=null ? new AddressReqDTO
                {
                    Street = request.Address.Street,
                    City = request.Address.City,
                    District = request.Address.District,
                    Province = request.Address.Province,
                    Country = request.Address.Country
                } : null,
                EmergencyContactName = request.EmergencyContactName,
                EmergencyContactNumber = request.EmergencyContactNumber,
                Status = request.Status,
            }).ToList();

            return new PaginatedResponse<NeworChangeMemberRequestResDTO>
            {
                TotalRecords = requestList.TotalRecords,
                PageNumber = requestList.PageNumber,
                PageSize = requestList.PageSize,
                Data = requestDTOList
            };
        }

        public async Task<PaginatedResponse<NeworChangeMemberRequestResDTO>> GetMemberInfoChangeRequestsAsync(int pageNumber, int pageSize)
        {

            var requestList = await _requestRepository.GetRequestByType("MemberInfo", pageNumber, pageSize);

            var requestDTOList = requestList.Data.Select(request => new NeworChangeMemberRequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                NIC = request.NIC,
                DOB = request.DOB,
                ImagePath = request.ImagePath,
                Password = request.Password,
                Gender = request.Gender,
                Address = request.Address != null ? new AddressReqDTO
                {
                    Street = request.Address.Street,
                    City = request.Address.City,
                    District = request.Address.District,
                    Province = request.Address.Province,
                    Country = request.Address.Country
                } : null,
                EmergencyContactName = request.EmergencyContactName,
                EmergencyContactNumber = request.EmergencyContactNumber,
                Status = request.Status,
            }).ToList();

            return new PaginatedResponse<NeworChangeMemberRequestResDTO>
            {
                TotalRecords = requestList.TotalRecords,
                PageNumber = requestList.PageNumber,
                PageSize = requestList.PageSize,
                Data = requestDTOList
            };
        }

        public async Task<PaginatedResponse<PaymentRequestResDTO>> GetPaymentRequestsAsync(int pageNumber, int pageSize)
        {

            var requestList = await _requestRepository.GetRequestByType("Payment", pageNumber, pageSize);

            var requestDTOList = requestList.Data.Select(request => new PaymentRequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                MemberId = request.MemberId,
                PaymentType = request.PaymentType,
                Amount = request.Amount,
                ReceiptNumber = request.ReceiptNumber,
                PaidDate = request.PaidDate,
                DueDate = request.DueDate,
                Status = request.Status,
            }).ToList();

            return new PaginatedResponse<PaymentRequestResDTO>
            {
                TotalRecords = requestList.TotalRecords,
                PageNumber = requestList.PageNumber,
                PageSize = requestList.PageSize,
                Data = requestDTOList
            };
        }


        public async Task<RequestResDTO> GetRequestById(int requestId)
        {
            var request = await _requestRepository.GetRequestById(requestId);

            var requestResDTO = new RequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                MemberId = request.MemberId,
                PaymentType = request.PaymentType,
                Amount = request.Amount,
                ReceiptNumber = request.ReceiptNumber,
                PaidDate = request.PaidDate,
                DueDate = request.DueDate,
                Status = request.Status,
                ProgramId = request.ProgramId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                NIC = request.NIC,
                Email = request.Email,
                DOB = request.DOB,
                Gender = request.Gender,
                Address = request.Address != null ? new AddressResDTO
                {
                    Street = request.Address.Street,
                    City = request.Address.City,
                    District = request.Address.District,
                    Province = request.Address.Province,
                    Country = request.Address.Country
                } : null,
                EmergencyContactName = request.EmergencyContactName,
                EmergencyContactNumber = request.EmergencyContactNumber,
                ImagePath = request.ImagePath,
                Password = request.Password,
            };

            return requestResDTO;
        }

        public async Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize)
        {
            var requests = await _requestRepository.GetRequestByType(requestType, pageNumber, pageSize);

            return requests;
        }

        public async Task<List<RequestResDTO>> GetAllRequests()
        {
            var requests = await _requestRepository.GetAllRequests();

            var requestResDTOList = requests.Select(request => new RequestResDTO
            {
                RequestId = request.RequestId,
                RequestType = request.RequestType,
                MemberId = request.MemberId,
                PaymentType = request.PaymentType,
                Amount = request.Amount,
                ReceiptNumber = request.ReceiptNumber,
                PaidDate = request.PaidDate,
                DueDate = request.DueDate,
                Status = request.Status,
                ProgramId = request.ProgramId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                NIC = request.NIC,
                Email = request.Email,
                DOB = request.DOB,
                Gender = request.Gender,
                Address = request.Address != null ? new AddressResDTO
                {
                    Street = request.Address.Street,
                    City = request.Address.City,
                    District = request.Address.District,
                    Province = request.Address.Province,
                    Country = request.Address.Country
                } : null,
                EmergencyContactName = request.EmergencyContactName,
                EmergencyContactNumber = request.EmergencyContactNumber,
                ImagePath = request.ImagePath,
                Password = request.Password
            }).ToList();

            return requestResDTOList;
        }

   

        public async Task<RequestResDTO> UpdateRequest(int requestId, Request updateRequest)
        {
            var existingRequest = await _requestRepository.GetRequestById(requestId);
            if (existingRequest == null)
            {
                throw new Exception("Request id is invalid");
            }
            existingRequest.Status = updateRequest.Status;

            await _requestRepository.UpdateRequest(existingRequest);

            var requestResDTO = new RequestResDTO
            {
                RequestId = existingRequest.RequestId,
                RequestType = existingRequest.RequestType,
                MemberId = existingRequest.MemberId,
                PaymentType = existingRequest.PaymentType,
                Amount = existingRequest.Amount,
                ReceiptNumber = existingRequest.ReceiptNumber,
                PaidDate = existingRequest.PaidDate,
                DueDate = existingRequest.DueDate,
                Status = existingRequest.Status,
                ProgramId = existingRequest.ProgramId,
                FirstName = existingRequest.FirstName,
                LastName = existingRequest.LastName,
                Phone = existingRequest.Phone,
                NIC = existingRequest.NIC,
                Email = existingRequest.Email,
                DOB = existingRequest.DOB,
                Gender = existingRequest.Gender,
                Address = existingRequest.Address != null ? new AddressResDTO
                {
                    Street = existingRequest.Address.Street,
                    City = existingRequest.Address.City,
                    District = existingRequest.Address.District,
                    Province = existingRequest.Address.Province,
                    Country = existingRequest.Address.Country
                } : null,
                EmergencyContactName = existingRequest.EmergencyContactName,
                EmergencyContactNumber = existingRequest.EmergencyContactNumber,
                ImagePath = existingRequest.ImagePath,
                Password = existingRequest.Password
            };

            return requestResDTO;
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

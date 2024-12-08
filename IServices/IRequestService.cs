using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.DTOs.Response.RequestResponseDTOs;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IRequestService
    {
        Task<RequestResDTO> GetRequestById(int requestId);
        Task<List<RequestResDTO>> GetAllRequests();
        //Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize);
        Task<RequestResDTO> UpdateRequest(int requestId, Request updateRequest);
        Task<NeworChangeMemberRequestResDTO> AddMemberRequestAsync(NeworChangeMemberRequestDTO requestDTO);
        Task<NeworChangeMemberRequestResDTO> ChangeMemberInfoRequestAsync(NeworChangeMemberRequestDTO requestDTO);
        Task<ProgramAddonRequestResDTO> AddProgramAddonRequestAsync(ProgramAddonRequestDTO requestDTO);
        Task<LeaveProgramRequestResDTO> AddLeaveProgramRequestAsync(LeaveProgramRequestDTO requestDTO);
        Task<PaymentRequestResDTO> AddPaymentRequestAsync(PaymentRequestDTO requestDTO);
        Task<PaginatedResponse<PaymentRequestResDTO>> GetPaymentRequestsAsync(int pageNumber, int pageSize);
        Task<PaginatedResponse<NeworChangeMemberRequestResDTO>> GetMemberInfoChangeRequestsAsync(int pageNumber, int pageSize);
        Task<PaginatedResponse<NeworChangeMemberRequestResDTO>> GetNewMemberRequestsAsync(int pageNumber, int pageSize);
        Task<PaginatedResponse<LeaveProgramRequestResDTO>> GetLeaveProgramRequestsAsync(int pageNumber, int pageSize);
        Task<PaginatedResponse<ProgramAddonRequestResDTO>> GetProgramAddonRequestsAsync(int pageNumber, int pageSize);
    }
}

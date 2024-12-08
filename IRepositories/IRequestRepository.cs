using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IRequestRepository
    {
        Task<Request> GetRequestById(int requestId);
        Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize);
        Task<List<Request>> GetAllRequests();
        Task<Request> AddRequest(Request request);
        Task<Request> UpdateRequest(Request request);
    }
}

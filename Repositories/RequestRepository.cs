using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly GymDbContext _dbContext;

        public RequestRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Request> GetRequestById(int requestId)
        {
            var request = await _dbContext.Requests.Include(r => r.Address).SingleOrDefaultAsync(r => r.RequestId == requestId);
            if (request == null)
            {
                throw new Exception("Request not Found!");
            }
            return request;

        }

        public async Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize)
        {
           
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.Requests
                .Where(r => r.RequestType == requestType)
                .CountAsync();

            if (totalRecords == 0)
            {
                throw new Exception("Requests not Found!");
            }

            
            var requests = await _dbContext.Requests
                .Where(r => r.RequestType == requestType)
                .Include(r => r.Address)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<Request>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = requests
            };

            return response;
        }


        public async Task<List<Request>> GetAllRequests()
        {
            var requests = await _dbContext.Requests.Include(r => r.Address).ToListAsync();
            if (requests.Count == 0)
            {
                throw new Exception("Requests not Found!");
            }
            return requests;

        }

        public async Task<Request> AddRequest(Request request)
        {
            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<Request> UpdateRequest(Request request)
        {
            var findedType = await GetRequestById(request.RequestId);
            if (findedType == null)
            {
                throw new Exception("Request not Found!");
            }
            _dbContext.Update(findedType);
            await _dbContext.SaveChangesAsync();
            return findedType;
        }
    }
}

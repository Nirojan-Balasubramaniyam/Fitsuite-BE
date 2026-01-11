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

        /*  public async Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize)
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
          }*/

        public async Task<PaginatedResponse<Request>> GetRequestByType(string requestType, int pageNumber, int pageSize)
        {
            var query = _dbContext.Requests
                .Where(r => r.RequestType == requestType)
                .Include(r => r.Address);

            // If pageNumber and pageSize are both 0, return all records
            if (pageNumber == 0 || pageSize == 0)
            {
                var allRequests = await query.ToListAsync(); // Get all requests without pagination
                var response1 = new PaginatedResponse<Request>
                {
                    TotalRecords = allRequests.Count,
                    PageNumber = 1,  // Defaulting to 1 as no pagination
                    PageSize = allRequests.Count, // Show total number of records
                    Data = allRequests
                };
                return response1;
            }

            // Apply pagination if pageNumber and pageSize are valid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await query.CountAsync();

            // Return empty list instead of throwing exception when no requests found
            if (totalRecords == 0)
            {
                return new PaginatedResponse<Request>
                {
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = new List<Request>()
                };
            }

            var requests = await query
                .Skip((pageNumber - 1) * pageSize)  // Pagination
                .Take(pageSize)  // Pagination
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
            // Return empty list instead of throwing exception when no requests found
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

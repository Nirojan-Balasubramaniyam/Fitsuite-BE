namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class PaginatedResponse<T>
    {
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ICollection<T> Data { get; set; }
    }
}

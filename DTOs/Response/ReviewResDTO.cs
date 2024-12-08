namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class ReviewResDTO
    {
        public string FullName { get; set; }
        public string ImagePath { get; set; }
        public string ReviewMessage { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

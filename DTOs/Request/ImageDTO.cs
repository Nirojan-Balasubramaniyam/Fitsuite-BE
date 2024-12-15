namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }  // URL of the image in Cloudinary
        public string FileName { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}

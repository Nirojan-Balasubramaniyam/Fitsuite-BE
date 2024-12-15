namespace GYMFeeManagement_System_BE.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }  // URL of the image in Cloudinary
        public string FileName { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}

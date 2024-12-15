using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GYMFeeManagement_System_BE.Entities;
using Microsoft.Extensions.Options;

namespace GYMFeeManagement_System_BE.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            var account = new Account(
                cloudinarySettings.Value.CloudName,
                cloudinarySettings.Value.ApiKey,
                cloudinarySettings.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        // Method to upload the image to Cloudinary and return the URL
        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, imageStream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill") // Optional transformation
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString(); // Return the URL of the uploaded image
            }
            else
            {
                throw new Exception("Failed to upload image to Cloudinary.");
            }
        }
    }
}

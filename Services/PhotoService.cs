using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using itransition_task6_server.Helpers;
using itransition_task6_server.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;

namespace itransition_task6_server.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acccount = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            cloudinary = new Cloudinary(acccount);
        }
        public async Task<string> AddPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation()
                    .Height(600)
                    .Width(600)
                    .Crop("fill")
                    .Quality("auto"),
                Folder = "itransition"
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != HttpStatusCode.OK || uploadResult.SecureUrl == null)
            {
                var err = uploadResult.Error?.Message ?? "Unknown error uploading to Cloudinary";
                throw new InvalidOperationException($"Cloudinary upload failed: {err}");
            }
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}

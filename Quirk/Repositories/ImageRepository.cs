using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Quirk.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public readonly IConfiguration _configuration;
        private readonly Account _cloudinaryAccount;
        public ImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinaryAccount = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_cloudinaryAccount);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,
            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if (uploadResult != null && uploadResult.StatusCode ==HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}

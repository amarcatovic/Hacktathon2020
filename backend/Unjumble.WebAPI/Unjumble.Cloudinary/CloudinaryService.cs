using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Unjumble.Core.Helper;
using Unjumble.Core.Interfaces;

namespace Unjumble.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;
        private CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinarySettings = cloudinarySettings;

            var account = new Account
               (
                   _cloudinarySettings.Value.CloudName,
                   _cloudinarySettings.Value.APIKey,
                   _cloudinarySettings.Value.APISecret
               );

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var uploadResult = new ImageUploadResult();
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }

                return uploadResult.Url.AbsoluteUri;
            }

            return null;
        }
    }
}

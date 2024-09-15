using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using ServiceContracts.DTO.UserProfile;
using ServiceContracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IOptions<CloudinarySettings> _config;
        private readonly Cloudinary _cloudinary;
        public UserService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account()
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            };

            _config = config;
            _cloudinary = new Cloudinary(acc);
        }
        public Task<UserProfileDTO> UpdateUserProfile(ApplicationUser user)
        {
            return null;
        }

        public async Task<ImageUploadResult> UploadImage(UserProfileUpdateDTO userProfile)
        {
            var uploadResult = new ImageUploadResult();
            if (userProfile.Image!.Length > 0)
            {
                using var stream = userProfile.Image.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(userProfile.Image.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}

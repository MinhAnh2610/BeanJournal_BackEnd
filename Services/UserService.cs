using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using ServiceContracts.DTO.UserProfile;
using ServiceContracts.Mapper;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IOptions<CloudinarySettings> config, UserManager<ApplicationUser> userManager)
        {
            var acc = new Account()
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            };

            _config = config;
            _cloudinary = new Cloudinary(acc);
            _userManager = userManager;
        }
        public async Task<UserProfileDTO> UpdateUserProfile(ApplicationUser user, UserProfileUpdateDTO userProfile)
        {
            if (!user.ProfileImagePublicId.IsNullOrEmpty())
            {
                await DeleteImage(user.ProfileImagePublicId);
            }

            var imageResult = await UploadImage(userProfile);
            user.UserName = userProfile.Username;
            user.ProfileImagePublicId = imageResult.PublicId;
            user.ProfileImageUrl = imageResult.Url.ToString();

            await _userManager.UpdateAsync(user);

            return user.ToUserProfile();
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

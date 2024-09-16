using CloudinaryDotNet.Actions;
using Entities;
using ServiceContracts.DTO.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IUserService
    {
        Task<UserProfileDTO> UpdateUserProfile(ApplicationUser user, UserProfileUpdateDTO userProfile);
        Task<ImageUploadResult> UploadImage(UserProfileUpdateDTO userProfile);
        Task<DeletionResult> DeleteImage(string publicId);
    }
}

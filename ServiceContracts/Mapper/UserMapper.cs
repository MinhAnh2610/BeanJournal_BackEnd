using Entities;
using ServiceContracts.DTO.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mapper
{
    public static class UserMapper
    {
        public static UserProfileDTO ToUserProfile(this ApplicationUser user)
        {
            return new UserProfileDTO()
            {
                CreatedAt = user.CreatedAt,
                Email = user.Email!,
                ProfileImageUrl = user.ProfileImageUrl,
                Username = user.UserName!
            };
        }
    }
}

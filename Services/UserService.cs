using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using ServiceContracts;
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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IImageRepository _imageRepository;

		public UserService(UserManager<ApplicationUser> userManager, IImageRepository imageRepository)
		{
			_userManager = userManager;
			_imageRepository = imageRepository;
		}

		public async Task<UserProfileDTO> UpdateUserProfile(ApplicationUser user, UserProfileUpdateDTO userProfile)
		{
			if (!user.ProfileImagePublicId.IsNullOrEmpty())
			{
				await _imageRepository.DeleteByPublicId(user.ProfileImagePublicId);
			}

			if (userProfile.Image != null)
			{
				var imageResult = await _imageRepository.UploadImage(userProfile.Image!, 500, 500);
				user.ProfileImagePublicId = imageResult.PublicId;
				user.ProfileImageUrl = imageResult.Url.ToString();
			}
			user.UserName = userProfile.Username;

			await _userManager.UpdateAsync(user);

			return user.ToUserProfile();
		}
	}
}

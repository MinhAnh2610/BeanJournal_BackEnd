using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using ServiceContracts;
using ServiceContracts.DTO.UserProfile;

namespace BeanJournal_BackEnd.Controllers
{
    /// <summary>
    /// User API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userService"></param>
        public UserController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }

        /// <summary>
        /// Update user profile API
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UserProfileUpdateDTO userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var existingUser = await _userManager.FindByNameAsync(userProfile.Username!);
            var updatedUser = new UserProfileDTO();
            if (existingUser == null)
            {
                updatedUser = await _userService.UpdateUserProfile(user, userProfile);   
                return Ok(updatedUser);
            }
            else if (existingUser!.Id != user.Id)
            { 
                return BadRequest("Username already taken");
            }
            else
            {
                updatedUser = await _userService.UpdateUserProfile(user, userProfile);
                return Ok(updatedUser);
            }
        }
    }
}

using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeanJournal_BackEnd.Controllers
{
    /// <summary>
    /// User API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}

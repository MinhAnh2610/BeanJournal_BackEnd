using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Account;

namespace BeanJournal_BackEnd.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    public AccountController(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             RoleManager<Role> roleManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    /// <summary>
    /// Login into the system
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var user = await _userManager.FindByEmailAsync(loginDto.Email);
      var loginResult = await _signInManager.CheckPasswordSignInAsync(user!, loginDto.Password, false);
      if (loginResult.Succeeded)
      {
        return Ok(new NewUserDTO()
        {
          UserName = user?.UserName!,
          Email = user?.Email!,
          Token = ""
        });
      }
      else
      {
        return StatusCode(500, "Incorrect username or password");
      }
    }

    /// <summary>
    /// Register a new account
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
      if (existingUser != null)
      {
        return BadRequest("Email is already registered");
      }
      var user = new User()
      {
        UserName = registerDto.UserName,
        Email = registerDto.Email
      };
      var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
      if (createdUser.Succeeded)
      {
        var roleResult = await _userManager.AddToRoleAsync(user, "User");
        if (roleResult.Succeeded)
        {
          return Ok(new NewUserDTO()
          {
            UserName = user.UserName,
            Email = user.Email,
            Token = ""
          });
        }
        else
        {
          return StatusCode(500, roleResult.Errors);
        }
      }
      else
      {
        return StatusCode(500, createdUser.Errors);
      }
    }

    /// <summary>
    /// Logout from system
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();

      return NoContent();
    }
  }
}

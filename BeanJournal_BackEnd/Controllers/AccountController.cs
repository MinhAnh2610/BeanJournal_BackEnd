using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.Account;
using System.Security.Claims;

namespace BeanJournal_BackEnd.Controllers
{
  /// <summary>
  /// Account APIs
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="roleManager"></param>
    /// <param name="tokenService"></param>
    public AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             RoleManager<ApplicationRole> roleManager,
                             ITokenService tokenService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _tokenService = tokenService;
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
      if (user == null)
      {
        return BadRequest("User not found");
      }
      var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
      if (loginResult.Succeeded)
      {
        var userRoles = await _userManager.GetRolesAsync(user);
        var userRole = userRoles.FirstOrDefault();

        var role = await _roleManager.FindByNameAsync(userRole!);

        var authentication = _tokenService.CreateJwtToken(user, role!);

        user.RefreshToken = authentication.RefreshToken;
        user.RefreshTokenExpirationDateTime = authentication.RefreshTokenExpirationDateTime;

        await _userManager.UpdateAsync(user);

        return Ok(authentication);
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
      var user = new ApplicationUser()
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
          var role = await _roleManager.FindByNameAsync("User");
          var authentication = _tokenService.CreateJwtToken(user, role!);

          user.RefreshToken = authentication.RefreshToken;
          user.RefreshTokenExpirationDateTime = authentication.RefreshTokenExpirationDateTime;

          await _userManager.UpdateAsync(user);

          return Ok(authentication);
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
    /// Enter email to generate a reset password token
    /// </summary>
    /// <param name="forgotPasswordDto"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
      if (user != null)
      {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return Ok(token);
      }
      return BadRequest("User not found");
    }

    /// <summary>
    /// Enter new password to change the old one
    /// </summary>
    /// <param name="resetPasswordDto"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
      if (user != null)
      {
        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPasswordResult.Succeeded)
        {
          return Ok("Password has been changed");
        }
        return StatusCode(500, "Something went wrong");
      }
      return BadRequest("User not found");
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

    /// <summary>
    /// Generate new access and refresh tokens from old ones
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    [HttpPost("generate-new-jwt-token")]
    public async Task<IActionResult> GenerateNewAccessToken([FromBody] TokenModel tokenModel)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (tokenModel == null)
      {
        return BadRequest("Invalid Client Request");
      }

      ClaimsPrincipal? principal = _tokenService.GetPrincipalFromJwtToken(tokenModel.Token);
      if (principal == null)
      {
        return BadRequest("Invalid jwt access token");
      }

      string? email = principal.FindFirstValue(ClaimTypes.Email);
      string? role = principal.FindFirstValue(ClaimTypes.Role);

      ApplicationRole? userRole = await _roleManager.FindByNameAsync(role!);
      ApplicationUser? user = await _userManager.FindByEmailAsync(email!);

      if (user == null || user.RefreshToken
        != tokenModel.RefreshToken
        || user.RefreshTokenExpirationDateTime <= DateTime.UtcNow)
      {
        return BadRequest("Invalid Refresh Token");
      }

      AuthenticationResponse authentication = _tokenService.CreateJwtToken(user, userRole!);

      user.RefreshToken = authentication.RefreshToken;
      user.RefreshTokenExpirationDateTime = authentication.RefreshTokenExpirationDateTime;

      await _userManager.UpdateAsync(user);

      return Ok(authentication);
    }
  }
}

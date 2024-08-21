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
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             RoleManager<Role> roleManager,
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
        var authentication = _tokenService.CreateJwtToken(user);

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
          var authentication = _tokenService.CreateJwtToken(user);

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
    /// Logout from system
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();

      return NoContent();
    }

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

      User? user = await _userManager.FindByEmailAsync(email!);

      if (user == null || user.RefreshToken 
        != tokenModel.RefreshToken 
        || user.RefreshTokenExpirationDateTime <= DateTime.UtcNow) 
      {
        return BadRequest("Invalid Refresh Token");
      }

      AuthenticationResponse authentication = _tokenService.CreateJwtToken(user);
      
      user.RefreshToken = authentication.RefreshToken;
      user.RefreshTokenExpirationDateTime = authentication.RefreshTokenExpirationDateTime;

      await _userManager.UpdateAsync(user);

      return Ok(authentication);
    }
  }
}

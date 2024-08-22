using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public AuthenticationResponse CreateJwtToken(ApplicationUser user, ApplicationRole role)
    {
      DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

      var claims = new List<Claim>()
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject (user id)
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), //Issued at (Date and time of token
                                                                            //generation)
        new Claim(ClaimTypes.NameIdentifier, user.Email!), //Unique name identifier of the user (Email)
        new Claim(ClaimTypes.Name, user.UserName!), //Name of the user
        new Claim(ClaimTypes.Email, user.Email!), //Email of the user
        new Claim(ClaimTypes.Role, role.Name!.ToString()) // Role of the user
      };

      SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

      SigningCredentials creds = new
        SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      SecurityTokenDescriptor tokenGenerator = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = expiration,
        SigningCredentials = creds,
        Issuer = _configuration["Jwt:Issuer"],
        Audience = _configuration["Jwt:Audience"]
      };

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      string token = tokenHandler.CreateEncodedJwt(tokenGenerator);

      return new AuthenticationResponse()
      {
        UserName = user.UserName!,
        Email = user.Email!,
        Token = token,
        Expiration = expiration,
        RefreshToken = GenerateRefreshToken(),
        RefreshTokenExpirationDateTime = DateTime.UtcNow.AddMinutes
        (Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"])),
      };
    }

    public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
    {
      var tokenValidationParameters = new TokenValidationParameters()
      {
        ValidateAudience = true,
        ValidAudience = _configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = _configuration["Jwt:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
        ValidateLifetime = false, // Should be false
      };

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      ClaimsPrincipal principal = tokenHandler
        .ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

      if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals
        (SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
      {
        throw new SecurityTokenException("Invalid Token");
      }

      return principal;
    }

    /// <summary>
    /// Create a refresh token (base 64 string of random numbers)
    /// </summary>
    /// <returns></returns>
    private string GenerateRefreshToken()
    {
      byte[] bytes = new byte[64];
      var random = RandomNumberGenerator.Create();
      random.GetBytes(bytes);
      return Convert.ToBase64String(bytes);
    }
  }
}

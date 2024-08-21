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
    public NewUserDTO CreateJwtToken(User user)
    {
      DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

      Claim[] claims = new Claim[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject (user id)
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), //Issued at (Date and time of token
                                                                            //generation)
        new Claim(ClaimTypes.NameIdentifier, user.Email!), //Unique name identifier of the user (Email)
        new Claim(ClaimTypes.Name, user.UserName!) //Name of the user
      };

      SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

      SigningCredentials signingCreadentials = new
        SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

      JwtSecurityToken tokenGenerator = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: expiration,
        signingCredentials: signingCreadentials
        );

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      string token = tokenHandler.WriteToken(tokenGenerator);

      return new NewUserDTO()
      {
        UserName = user.UserName!,
        Email = user.Email!,
        Token = token,
        Expiration = expiration
      };
    }
  }
}

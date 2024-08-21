using Entities;
using ServiceContracts.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
  public interface ITokenService
  {
    AuthenticationResponse CreateJwtToken(User user);
    ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
  }
}

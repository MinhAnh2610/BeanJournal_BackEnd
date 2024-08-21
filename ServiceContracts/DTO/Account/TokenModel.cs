using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Account
{
  public class TokenModel
  {
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
  }
}

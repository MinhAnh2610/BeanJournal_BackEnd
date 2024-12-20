﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Account
{
  public class ForgotPasswordDTO
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Tag
{
  public class TagDTO
  {
    public int TagId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImagePublicId { get; set; } = string.Empty;
    public string IconPublicId { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
  }
}

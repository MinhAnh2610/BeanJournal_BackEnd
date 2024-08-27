using Microsoft.AspNetCore.Http;
using ServiceContracts.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Tag
{
  public class TagUpdateDTO
  {
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [ImageFile(new string[] { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" })]
    public IFormFile? Image { get; set; }
    [Required]
    [ImageFile(new string[] { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" })]
    public IFormFile? Icon { get; set; }
  }
}

using Microsoft.AspNetCore.Http;
using ServiceContracts.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.UserProfile
{
    public class UserProfileUpdateDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [ImageFile(new string[] { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" })]
        public IFormFile? Image { get; set; }
    }
}

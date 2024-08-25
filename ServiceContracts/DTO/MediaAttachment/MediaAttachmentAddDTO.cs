using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceContracts.ValidationAttributes;

namespace ServiceContracts.DTO.MediaAttachment
{
  public class MediaAttachmentAddDTO
  {
    [Required]
    [ImageFile(new string[] { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" })]
    public IFormFile? File { get; set; }
  }
}

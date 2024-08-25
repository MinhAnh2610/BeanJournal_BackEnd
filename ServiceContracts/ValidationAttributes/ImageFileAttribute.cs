using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ValidationAttributes
{
  public class ImageFileAttribute : ValidationAttribute
  {
    private readonly string[] _extensions = { };
    public ImageFileAttribute(string[] extensions)
    {
      _extensions = extensions;
    }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
      var file = value as IFormFile;
      var extension = Path.GetExtension(file!.FileName);
      if (!_extensions.Contains(extension.ToLower()))
      {
        return new ValidationResult("Your image's file type is not valid");
      }
      return ValidationResult.Success!;
    }
  }
}

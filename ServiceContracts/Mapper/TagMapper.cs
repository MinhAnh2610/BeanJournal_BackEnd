using CloudinaryDotNet.Actions;
using Entities;
using ServiceContracts.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mapper
{
  public static class TagMapper
  {
    public static Tag ToTagFromAdd(this TagAddDTO tagAddRequest, ImageUploadResult imageResult, ImageUploadResult iconResult)
    {
      return new Tag
      {
        Name = tagAddRequest.Name,
        ImagePublicId = imageResult.PublicId,
        ImageUrl = imageResult.Url.ToString(),
        IconPublicId = iconResult.PublicId,
        IconUrl = iconResult.Url.ToString(),
      };
    }

    public static TagDTO ToTagDto(this Tag tag)
    {
      return new TagDTO
      {
        TagId = tag.TagId,
        Name = tag.Name,
        ImageUrl = tag.ImageUrl,
        IconUrl = tag.IconUrl,
        ImagePublicId = tag.IconPublicId,
        IconPublicId = tag.IconPublicId,
      };
    }
  }
}

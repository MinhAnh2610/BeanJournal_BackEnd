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
    public static Tag ToTagFromAdd(this TagAddDTO tagAddRequest)
    {
      return new Tag
      {
        Name = tagAddRequest.Name
      };
    }

    public static Tag ToTagFromUpdate(this TagUpdateDTO tagUpdateRequest)
    {
      return new Tag
      {
        Name = tagUpdateRequest.Name
      };
    }

    public static TagDTO ToTagDto(this Tag tag)
    {
      return new TagDTO
      {
        TagId = tag.TagId,
        Name = tag.Name
      };
    }
  }
}

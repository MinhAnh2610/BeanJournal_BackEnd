using Entities;
using ServiceContracts.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
  public interface ITagService
  {
    Task<TagDTO> AddTag(TagAddDTO tag);
    Task<ICollection<TagDTO>?> GetAllTags();
    Task<TagDTO?> GetTagById(int id);
    Task<TagDTO?> UpdateTag(int tagId, TagUpdateDTO tag);
    Task<TagDTO?> DeleteTag(int id);
  }
}

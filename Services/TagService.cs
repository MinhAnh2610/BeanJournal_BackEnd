using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class TagService : ITagService
  {
    private readonly ITagRepository _tagRepository;
    public TagService(ITagRepository tagRepository)
    {
      _tagRepository = tagRepository;
    }

    public Task<TagDTO> AddTag(TagAddDTO tag)
    {
      throw new NotImplementedException();
    }

    public Task<TagDTO?> DeleteTag(int id)
    {
      throw new NotImplementedException();
    }

    public Task<ICollection<TagDTO>?> GetAllTags()
    {
      throw new NotImplementedException();
    }

    public Task<TagDTO?> GetTagById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<TagDTO?> UpdateTag(TagUpdateDTO tag)
    {
      throw new NotImplementedException();
    }
  }
}

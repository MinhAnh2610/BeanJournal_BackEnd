using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using ServiceContracts.Mapper;
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

    public async Task<TagDTO> AddTag(TagAddDTO tag)
    {
      var tagModel = tag.ToTagFromAdd();
      var tagResponse = await _tagRepository.CreateTagAsync(tagModel);
      return tagResponse.ToTagDto();
    }

    public async Task<TagDTO?> DeleteTag(int id)
    {
      var tagReponse = await _tagRepository.DeleteTagAsync(id);
      if (tagReponse == null)
      {
        return null;
      }
      return tagReponse.ToTagDto();
    }

    public async Task<ICollection<TagDTO>?> GetAllTags()
    {
      var tagResponses = await _tagRepository.GetTagsAsync();
      if (tagResponses == null)
      {
        return null;
      }
      return tagResponses.Select(x => x.ToTagDto()).ToList();
    }

    public async Task<TagDTO?> GetTagById(int id)
    {
      var tagResponse = await _tagRepository.GetTagByIdAsync(id);
      if (tagResponse == null)
      {
        return null;
      }
      return tagResponse.ToTagDto();
    }

    public async Task<TagDTO?> UpdateTag(int tagId, TagUpdateDTO tag)
    {
      var tagModel = tag.ToTagFromUpdate();
      var tagResponse = await _tagRepository.UpdateTagAsync(tagId, tagModel);
      if (tagResponse == null)
      {
        return null;
      }
      return tagResponse.ToTagDto();
    }
  }
}

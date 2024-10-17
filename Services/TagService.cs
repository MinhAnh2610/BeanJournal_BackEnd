using Microsoft.IdentityModel.Tokens;
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
		private readonly IImageRepository _imageRepository;
    public TagService(ITagRepository tagRepository, IImageRepository imageRepository)
    {
      _tagRepository = tagRepository;
			_imageRepository = imageRepository;
    }

    public async Task<TagDTO> AddTag(TagAddDTO tag)
    {
			var existingTag = await _tagRepository.GetTagByNameAsync(tag.Name);
			if (existingTag != null)
			{
				throw new ArgumentException();
			}

			var imageResult = await _imageRepository.UploadImage(tag.Image!, 500, 500);
      var iconResult = await _imageRepository.UploadImage(tag.Icon!, 100, 100);

      var tagModel = tag.ToTagFromAdd(imageResult, iconResult);

      var tagResponse = await _tagRepository.CreateTagAsync(tagModel);
      return tagResponse.ToTagDto();
    }

    public async Task<TagDTO?> DeleteTag(int id)
    {
      var existingTag = await _tagRepository.GetTagByIdAsync(id);
      if (existingTag == null)
      {
        return null;
      }

      if (!existingTag.ImagePublicId.IsNullOrEmpty())
      {
        await _imageRepository.DeleteByPublicId(existingTag.ImagePublicId);
      }
      if (!existingTag.IconPublicId.IsNullOrEmpty())
      {
        await _imageRepository.DeleteByPublicId(existingTag.IconPublicId);
      }

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

    public async Task<TagDTO?> UpdateTag(int tagId, TagAddDTO tag)
    {
      var existingTag = await _tagRepository.GetTagByIdAsync(tagId);
			var duplicateNameTag = await _tagRepository.GetTagByNameAsync(tag.Name);
			if (duplicateNameTag != null && duplicateNameTag.TagId != tagId)
			{
				throw new ArgumentException();
			}
			if (existingTag == null)
      {
        return null;
      }

      if (!existingTag.ImagePublicId.IsNullOrEmpty())
      {
        await _imageRepository.DeleteByPublicId(existingTag.ImagePublicId);
      }
      if (!existingTag.IconPublicId.IsNullOrEmpty())
      {
        await _imageRepository.DeleteByPublicId(existingTag.IconPublicId);
      }

      var imageResult = await _imageRepository.UploadImage(tag.Image!, 500, 500);
      var iconResult = await _imageRepository.UploadImage(tag.Icon!, 100, 100);

      var tagModel = tag.ToTagFromAdd(imageResult, iconResult);

      var tagResponse = await _tagRepository.UpdateTagAsync(tagId, tagModel);
      if (tagResponse == null)
      {
        return null;
      }
      return tagResponse.ToTagDto();
    }
  }
}

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.Extensions.Options;
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
    private readonly IOptions<CloudinarySettings> _config;
    private readonly Cloudinary _cloudinary;
    public TagService(ITagRepository tagRepository, IOptions<CloudinarySettings> config)
    {
      var acc = new Account()
      {
        Cloud = config.Value.CloudName,
        ApiKey = config.Value.ApiKey,
        ApiSecret = config.Value.ApiSecret
      };

      _tagRepository = tagRepository;
      _config = config;
      _cloudinary = new Cloudinary(acc);
    }

    public async Task<TagDTO> AddTag(TagAddDTO tag)
    {
      var imageResult = await UploadImage(tag);
      var iconResult = await UploadIcon(tag);

      var tagModel = tag.ToTagFromAdd(imageResult, iconResult);

      var tagResponse = await _tagRepository.CreateTagAsync(tagModel);
      return tagResponse.ToTagDto();
    }

    public async Task<DeletionResult> DeleteIcon(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);
      var result = await _cloudinary.DestroyAsync(deleteParams);

      return result;
    }

    public async Task<DeletionResult> DeleteImage(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);
      var result = await _cloudinary.DestroyAsync(deleteParams);

      return result;
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
        await DeleteImage(existingTag.ImagePublicId);
      }
      if (!existingTag.IconPublicId.IsNullOrEmpty())
      {
        await DeleteIcon(existingTag.IconPublicId);
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

      if (existingTag == null)
      {
        return null;
      }

      if (!existingTag.ImagePublicId.IsNullOrEmpty())
      {
        await DeleteImage(existingTag.ImagePublicId);
      }
      if (!existingTag.IconPublicId.IsNullOrEmpty())
      {
        await DeleteIcon(existingTag.IconPublicId);
      }

      var imageResult = await UploadImage(tag);
      var iconResult = await UploadIcon(tag);

      var tagModel = tag.ToTagFromAdd(imageResult, iconResult);

      var tagResponse = await _tagRepository.UpdateTagAsync(tagId, tagModel);
      if (tagResponse == null)
      {
        return null;
      }
      return tagResponse.ToTagDto();
    }

    public async Task<ImageUploadResult> UploadIcon(TagAddDTO tag)
    {
      var uploadResult = new ImageUploadResult();
      if (tag.Icon!.Length > 0)
      {
        using var stream = tag.Icon.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
          File = new FileDescription(tag.Icon.FileName, stream),
          Transformation = new Transformation().Height(100).Width(100).Crop("fill").Gravity("face")
        };
        uploadResult = await _cloudinary.UploadAsync(uploadParams);
      }
      return uploadResult;
    }

    public async Task<ImageUploadResult> UploadImage(TagAddDTO tag)
    {
      var uploadResult = new ImageUploadResult();
      if (tag.Image!.Length > 0)
      {
        using var stream = tag.Image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
          File = new FileDescription(tag.Image.FileName, stream),
          Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
        };
        uploadResult = await _cloudinary.UploadAsync(uploadParams);
      }
      return uploadResult;
    }
  }
}

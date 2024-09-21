using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.Extensions.Options;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.MediaAttachment;
using ServiceContracts.Mapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class MediaAttachmentService : IMediaAttachmentService
  {
    private readonly IMediaAttachmentRepository _mediaAttachmentRepository;
    private readonly IDiaryEntryRepository _entryRepository;
    private readonly IOptions<CloudinarySettings> _config;
    private readonly Cloudinary _cloudinary;
    public MediaAttachmentService(IMediaAttachmentRepository mediaAttachmentRepository,
                                  IDiaryEntryRepository entryRepository,
                                  IOptions<CloudinarySettings> config)
    {
      var acc = new Account()
      {
        Cloud = config.Value.CloudName,
        ApiKey = config.Value.ApiKey,
        ApiSecret = config.Value.ApiSecret
      };

      _mediaAttachmentRepository = mediaAttachmentRepository;
      _entryRepository = entryRepository;
      _config = config;
      _cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> UploadImage(MediaAttachmentAddDTO mediaAttachment)
    {
      var uploadResult = new ImageUploadResult();
      if (mediaAttachment.File!.Length > 0)
      {
        using var stream = mediaAttachment.File.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
          File = new FileDescription(mediaAttachment.File.FileName, stream),
          Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
        };
        uploadResult = await _cloudinary.UploadAsync(uploadParams);
      }
      return uploadResult;
    }

    public async Task<DeletionResult> DeleteImage(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);
      var result = await _cloudinary.DestroyAsync(deleteParams);

      return result;
    }

    public async Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachments()
    {
      var mediaAttachmentResponses = await _mediaAttachmentRepository.GetMediaAttachmentsAsync();
      if (mediaAttachmentResponses == null)
      {
        return null;
      }
      return mediaAttachmentResponses.Select(x => x.ToMediaAttachmentDto()).ToList();
    }

    public async Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachmentsByUser(string userId)
    {
      var mediaAttachmentResponses = await _mediaAttachmentRepository.GetMediaAttachmentsByUserAsync(userId);
      if (mediaAttachmentResponses == null)
      {
        return null;
      }
      return mediaAttachmentResponses.Select(x => x.ToMediaAttachmentDto()).ToList();
    }

    public async Task<MediaAttachmentDTO?> GetMediaAttachmentById(int id)
    {
      var mediaAttachmentResponse = await _mediaAttachmentRepository.GetMediaAttachmentByIdAsync(id);
      if (mediaAttachmentResponse == null)
      {
        return null;
      }
      return mediaAttachmentResponse.ToMediaAttachmentDto();
    }

    public async Task<ICollection<MediaAttachmentDTO>?> AddMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentAddDtos, int entryId)
    {
      string[] extensions = { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" };
      foreach (var media in mediaAttachmentAddDtos)
      {
        string extension = Path.GetExtension(media.File!.FileName);
        if (!extensions.Contains(extension))
        {
          return null;
        }
      }

      ICollection<MediaAttachmentDTO> mediaAttachmentResponses = new Collection<MediaAttachmentDTO>();
      foreach (var media in mediaAttachmentAddDtos)
      {
        var result = await UploadImage(media);
        var mediaAttachmentModel = result.ToMediaAttachmentFromAdd(entryId);

        var mediaAttachmentResponse = await _mediaAttachmentRepository.CreateMediaAttachmentAsync(mediaAttachmentModel);
        mediaAttachmentResponses.Add(mediaAttachmentResponse.ToMediaAttachmentDto());
      }

      return mediaAttachmentResponses.ToList();
    }

    public async Task<MediaAttachmentDTO?> DeleteMediaAttachment(int id)
    {
      var mediaAttachmentResponse = await _mediaAttachmentRepository.DeleteMediaAttachmentAsync(id);
      if (mediaAttachmentResponse == null)
      {
        return null;
      }
      var result = await DeleteImage(mediaAttachmentResponse.PublicId);
      return mediaAttachmentResponse.ToMediaAttachmentDto();
    }

    public async Task<ICollection<MediaAttachmentDTO>?> UpdateMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentUpdateDTOs, int entryId)
    {
      string[] extensions = { ".png", ".svg", ".gif", ".jpeg", ".jpg", ".tiff", ".psd", ".eps", ".raw" };
      foreach (var media in mediaAttachmentUpdateDTOs)
      {
        string extension = Path.GetExtension(media.File!.FileName);
        if (!extensions.Contains(extension))
        {
          return null;
        }
      }

      var existingMedias = await _mediaAttachmentRepository.GetMediaAttachmentsByEntryAsync(entryId);
      if (existingMedias != null)
      {
        foreach (var media in existingMedias)
        {
          await DeleteMediaAttachment(media.MediaId);
        }
      }

      ICollection<MediaAttachmentDTO> mediaAttachmentResponses = new Collection<MediaAttachmentDTO>();
      foreach (var media in mediaAttachmentUpdateDTOs)
      {
        var result = await UploadImage(media);
        var mediaAttachmentModel = result.ToMediaAttachmentFromAdd(entryId);

        var mediaAttachmentResponse = await _mediaAttachmentRepository.CreateMediaAttachmentAsync(mediaAttachmentModel);
        mediaAttachmentResponses.Add(mediaAttachmentResponse.ToMediaAttachmentDto());
      }

      return mediaAttachmentResponses.ToList();
    }
  }
}

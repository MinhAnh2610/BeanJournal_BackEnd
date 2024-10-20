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
    private readonly IImageRepository _imageRepository;

    public MediaAttachmentService(IMediaAttachmentRepository mediaAttachmentRepository,
																	IImageRepository imageRepository)
    {
      _mediaAttachmentRepository = mediaAttachmentRepository;
			_imageRepository = imageRepository;
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
        var result = await _imageRepository.UploadImage(media.File!, 500, 500);
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
      var result = await _imageRepository.DeleteByPublicId(mediaAttachmentResponse.PublicId);
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
        var result = await _imageRepository.UploadImage(media.File!, 500, 500);
        var mediaAttachmentModel = result.ToMediaAttachmentFromAdd(entryId);

        var mediaAttachmentResponse = await _mediaAttachmentRepository.CreateMediaAttachmentAsync(mediaAttachmentModel);
        mediaAttachmentResponses.Add(mediaAttachmentResponse.ToMediaAttachmentDto());
      }

      return mediaAttachmentResponses.ToList();
    }
  }
}

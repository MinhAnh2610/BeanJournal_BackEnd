using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.MediaAttachment;
using ServiceContracts.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class MediaAttachmentService : IMediaAttachmentService
  {
    private readonly IMediaAttachmentRepository _mediaAttachmentRepository;
    public MediaAttachmentService(IMediaAttachmentRepository mediaAttachmentRepository)
    {
      _mediaAttachmentRepository = mediaAttachmentRepository;
    }
    public Task<MediaAttachmentDTO> AddMediaAttachment(MediaAttachmentAddDTO mediaAttachment)
    {
      throw new NotImplementedException();
    }

    public Task<MediaAttachmentDTO?> DeleteMediaAttachment(int id)
    {
      throw new NotImplementedException();
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

    public async Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachmentsByUser(string id)
    {
      var mediaAttachmentResponses = await _mediaAttachmentRepository.GetMediaAttachmentsByUserAsync(id);
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

    public Task<MediaAttachmentDTO?> UpdateMediaAttachment(MediaAttachmentUpdateDTO mediaAttachment)
    {
      throw new NotImplementedException();
    }
  }
}

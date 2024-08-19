using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.MediaAttachment;
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

    public Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachments()
    {
      throw new NotImplementedException();
    }

    public Task<MediaAttachmentDTO?> GetMediaAttachmentById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<MediaAttachmentDTO?> UpdateMediaAttachment(MediaAttachmentUpdateDTO mediaAttachment)
    {
      throw new NotImplementedException();
    }
  }
}

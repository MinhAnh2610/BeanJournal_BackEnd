using ServiceContracts.DTO.MediaAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
  public interface IMediaAttachmentService
  {
    Task<MediaAttachmentDTO> AddMediaAttachment(MediaAttachmentAddDTO mediaAttachment);
    Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachments();
    Task<MediaAttachmentDTO?> GetMediaAttachmentById(int id);
    Task<MediaAttachmentDTO?> UpdateMediaAttachment(MediaAttachmentUpdateDTO mediaAttachment);
    Task<MediaAttachmentDTO?> DeleteMediaAttachment(int id);
  }
}

using CloudinaryDotNet.Actions;
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
    Task<ICollection<MediaAttachmentDTO>?> AddMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentAddDtos, int entryId);
    Task<ICollection<MediaAttachmentDTO>?> UpdateMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentUpdateDTOs, int entryId);
    Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachments();
    Task<MediaAttachmentDTO?> GetMediaAttachmentById(int id);
    Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachmentsByUser(string userId);
    Task<MediaAttachmentDTO?> DeleteMediaAttachment(int id);
  }
}

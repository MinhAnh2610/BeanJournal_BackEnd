using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
  public interface IMediaAttachmentRepository
  {
    Task<MediaAttachment> CreateMediaAttachmentAsync(MediaAttachment mediaAttachment);
    Task<ICollection<MediaAttachment>?> GetMediaAttachmentsAsync();
    Task<MediaAttachment?> GetMediaAttachmentByIdAsync(int id);
    Task<ICollection<MediaAttachment>?> GetMediaAttachmentsByUserAsync(string id);
    Task<ICollection<MediaAttachment>?> GetMediaAttachmentsByEntryAsync(int entryId);
    Task<MediaAttachment?> UpdateMediaAttachmentAsync(int mediaId, MediaAttachment mediaAttachment);
    Task<MediaAttachment?> DeleteMediaAttachmentAsync(int id);
  }
}

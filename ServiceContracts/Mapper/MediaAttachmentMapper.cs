using Entities;
using ServiceContracts.DTO.MediaAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mapper
{
  public static class MediaAttachmentMapper
  {
    public static MediaAttachmentDTO ToMediaAttachmentDto(this MediaAttachment mediaAttachment)
    {
      return new MediaAttachmentDTO
      {
        MediaId = mediaAttachment.MediaId,
        UserName = mediaAttachment.Entry!.UserId,
        EntryTitle = mediaAttachment.Entry!.Title,
        FilePath = mediaAttachment.FilePath,
        FileType = mediaAttachment.FileType,  
        CreatedAt = mediaAttachment.CreatedAt
      };
    }
  }
}

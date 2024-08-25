using CloudinaryDotNet.Actions;
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
    public static MediaAttachment ToMediaAttachmentFromAdd(this ImageUploadResult result, int entryId)
    {
      return new MediaAttachment
      {
        PublicId = result.PublicId,
        Bytes = result.Bytes,
        CreatedAt = result.CreatedAt,
        EntryId = entryId,
        FilePath = result.SecureUrl.ToString(),
        FileType = result.Format,
        Width = result.Width,
        Height = result.Height
      };
    }
    public static MediaAttachmentDTO ToMediaAttachmentDto(this MediaAttachment mediaAttachment)
    {
      return new MediaAttachmentDTO
      {
        MediaId = mediaAttachment.MediaId,
        Width = mediaAttachment.Width,
        Height = mediaAttachment.Height,
        Bytes = mediaAttachment.Bytes,
        FilePath = mediaAttachment.FilePath,
        FileType = mediaAttachment.FileType,  
        CreatedAt = mediaAttachment.CreatedAt
      };
    }
  }
}

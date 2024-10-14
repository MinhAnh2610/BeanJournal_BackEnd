using Entities;
using ServiceContracts.DTO.DiaryEntry;
using ServiceContracts.DTO.MediaAttachment;
using ServiceContracts.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mapper
{
  public static class DiaryEntryMapper
  {
    public static DiaryEntry ToDiaryEntryFromAdd(this DiaryEntryAddDTO entryAddRequest, string userId)
    {
      return new DiaryEntry
      {
        Content = entryAddRequest.Content,
        CreatedAt = entryAddRequest.CreatedAt,
        UpdatedAt = DateTime.Now,
        Mood = entryAddRequest.Mood,
        Title = entryAddRequest.Title,
        UserId = userId
      };
    }

    public static DiaryEntry ToDiaryEntryFromUpdate(this DiaryEntryUpdateDTO entryUpdateRequest, string userId)
    {
      return new DiaryEntry
      {
        Title = entryUpdateRequest.Title,
        Content = entryUpdateRequest.Content,
        Mood = entryUpdateRequest.Mood,
        UpdatedAt = entryUpdateRequest.UpdatedAt,
        UserId = userId
      };
    }

    public static DiaryEntryDTO ToDiaryEntryDto(this DiaryEntry entry)
    {
      return new DiaryEntryDTO
      {
        EntryId = entry.EntryId,
        Content = entry.Content,
        Mood = entry.Mood,
        Title = entry.Title,
        CreatedAt = entry.CreatedAt,
        UpdatedAt = entry.UpdatedAt,
        Tags = entry.EntryTags?
          .Select(x => new TagDTO()
          {
            TagId = x.TagId,
            Name = x.Tag!.Name,
						IconPublicId = x.Tag!.IconPublicId,
						IconUrl = x.Tag!.IconUrl,
						ImagePublicId = x.Tag!.ImagePublicId,
						ImageUrl = x.Tag!.ImageUrl
          })
          .ToList()!,
        MediaAttachments = entry.MediaAttachments?
          .Select(x => new MediaAttachmentDTO()
          {
            MediaId = x.MediaId,
            PublicId = x.PublicId,
            Width = x.Width,
            Height = x.Height,
            Bytes = x.Bytes,
            FilePath = x.FilePath,
            FileType = x.FileType,
            CreatedAt = x.CreatedAt
          })
          .ToList()!
      };
    }
  }
}

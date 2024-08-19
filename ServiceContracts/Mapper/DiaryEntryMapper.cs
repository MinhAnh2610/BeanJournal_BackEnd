using Entities;
using ServiceContracts.DTO.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mapper
{
  public static class DiaryEntryMapper
  {
    public static DiaryEntry ToDiaryEntryFromAdd(this DiaryEntryAddDTO entryAddRequest)
    {
      return new DiaryEntry
      {
        Content = entryAddRequest.Content,
        CreatedAt = entryAddRequest.CreatedAt,
        Mood = entryAddRequest.Mood,
        Title = entryAddRequest.Title,
      };
    }

    public static DiaryEntry ToDiaryEntryFromUpdate(this DiaryEntryUpdateDTO entryUpdateRequest)
    {
      return new DiaryEntry
      {
        Title = entryUpdateRequest.Title,
        Content = entryUpdateRequest.Content,
        Mood = entryUpdateRequest.Mood,
        UpdatedAt = entryUpdateRequest.UpdatedAt
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
        Username = entry.User?.UserName!
      };
    }
  }
}

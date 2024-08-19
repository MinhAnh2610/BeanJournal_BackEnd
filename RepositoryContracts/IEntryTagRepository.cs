using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
  public interface IEntryTagRepository
  {
    Task<EntryTag> CreateEntryTagAsync(EntryTag entryTag);
    Task<ICollection<EntryTag>?> GetEntryTagsAsync();
    Task<ICollection<EntryTag>?> GetEntryTagsByEntriesAsync(DiaryEntry entry);
    Task<ICollection<EntryTag>?> GetEntryTagsByTagsAsync(Tag tag);
    Task<EntryTag?> UpdateEntryTagAsync(EntryTag entryTag);
    Task<EntryTag?> DeleteEntryTagAsync(int id);
  }
}

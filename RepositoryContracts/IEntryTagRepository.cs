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
    Task<EntryTag> AddEntryTagAsync(EntryTag entryTag);
    Task<ICollection<EntryTag>?> DeleteEntryTagsAsync(int entryId);
    Task<EntryTag?> DeleteEntryTagByTagAndEntryAsync(int entryId, int tagId);
    Task<ICollection<EntryTag>?> GetEntryTagByEntryIdAsync(int entryId);
  }
}

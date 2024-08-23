using Entities;

namespace RepositoryContracts
{
  public interface IDiaryEntryRepository
  {
    Task<DiaryEntry> AddDiaryEntryAsync(DiaryEntry entry);
    Task<ICollection<DiaryEntry>?> GetDiaryEntriesAsync();
    Task<DiaryEntry?> GetDiaryEntryByIdAsync(int id);
    Task<DiaryEntry?> GetDiaryEntryByDateAsync(DateTime date);
    Task<ICollection<DiaryEntry>?> GetDiaryEntriesByUserAsync(string id); 
    Task<DiaryEntry?> UpdateDiaryEntryAsync(int entryId, DiaryEntry entry);
    Task<DiaryEntry?> DeleteDiaryEntryAsync(int id);
  }
}

using Entities;

namespace RepositoryContracts
{
  public interface IDiaryEntryRepository
  {
    Task<DiaryEntry> AddDiaryEntryAsync(DiaryEntry entry);
    Task<ICollection<DiaryEntry>?> GetDiaryEntriesAsync();
    Task<DiaryEntry?> GetDiaryEntryByIdAsync(int id);
    Task<DiaryEntry?> UpdateDiaryEntryAsync(DiaryEntry entry);
    Task<DiaryEntry?> DeleteDiaryEntryAsync(int id);
  }
}

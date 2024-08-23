using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;
using ServiceContracts.Mapper;

namespace Services
{
  public class DiaryEntryService : IDiaryEntryService
  {
    private readonly IDiaryEntryRepository _entryRepository;
    public DiaryEntryService(IDiaryEntryRepository entryRepository)
    {
      _entryRepository = entryRepository;
    }
    public async Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry)
    {
      var entryModel = entry.ToDiaryEntryFromAdd();
      var entryResponse = await _entryRepository.AddDiaryEntryAsync(entryModel);
      return entryResponse.ToDiaryEntryDto();
    }

    public async Task<DiaryEntryDTO?> DeleteDiaryEntry(int id)
    {
      var entryResponse = await _entryRepository.DeleteDiaryEntryAsync(id);
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.ToDiaryEntryDto();
    }

    public async Task<ICollection<DiaryEntryDTO>?> GetAllDiaryEntries()
    {
      var entryResponse = await _entryRepository.GetDiaryEntriesAsync();
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.Select(x => x.ToDiaryEntryDto()).ToList();
    }

    public async Task<DiaryEntryDTO?> GetDiaryEntryByDate(DateTime date)
    {
      var entryResponse = await _entryRepository.GetDiaryEntryByDateAsync(date);
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.ToDiaryEntryDto();
    }

    public async Task<DiaryEntryDTO?> GetDiaryEntryById(int id)
    {
      var entryResponse = await _entryRepository.GetDiaryEntryByIdAsync(id);
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.ToDiaryEntryDto();
    }

    public async Task<ICollection<DiaryEntryDTO>?> GetDiaryEntryByUserId(string userId)
    {
      var entryResponse = await _entryRepository.GetDiaryEntriesByUserAsync(userId);
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.Select(x => x.ToDiaryEntryDto()).ToList();
    }

    public async Task<DiaryEntryDTO?> UpdateDiaryEntry(int entryId, DiaryEntryUpdateDTO entry)
    {
      var entryModel = entry.ToDiaryEntryFromUpdate();
      var entryResponse = await _entryRepository.UpdateDiaryEntryAsync(entryId, entryModel);
      if (entryResponse == null)
      {
        return null;
      }
      return entryResponse.ToDiaryEntryDto();
    }
  }
}

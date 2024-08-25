using Entities;
using ServiceContracts.DTO.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
  public interface IDiaryEntryService
  {
    Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry, string userId);
    Task<ICollection<DiaryEntryDTO>?> GetAllDiaryEntries();
    Task<DiaryEntryDTO?> GetDiaryEntryById(int id);
    Task<DiaryEntryDTO?> GetDiaryEntryByDate(DateTime date);
    Task<ICollection<DiaryEntryDTO>?> GetDiaryEntryByUserId(string userId);
    Task<DiaryEntryDTO?> UpdateDiaryEntry(int entryId, DiaryEntryUpdateDTO entry, string userId);
    Task<DiaryEntryDTO?> DeleteDiaryEntry(int id);
  }
}

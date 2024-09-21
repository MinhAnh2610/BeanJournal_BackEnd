using Microsoft.Extensions.Caching.Memory;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Caching
{
    public class CachedDiaryEntryService : IDiaryEntryService
    {
        private readonly IDiaryEntryService _decorator;
        private readonly IMemoryCache _memoryCache;
        public CachedDiaryEntryService(IDiaryEntryService decorator, IMemoryCache memoryCache)
        {
            _decorator = decorator;
            _memoryCache = memoryCache;
        }

        public Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry, string userId)
        {
            return _decorator.AddDiaryEntry(entry, userId);
        }

        public Task<DiaryEntryDTO?> DeleteDiaryEntry(int id)
        {
            return _decorator.DeleteDiaryEntry(id);
        }

        public Task<ICollection<DiaryEntryDTO>?> GetAllDiaryEntries()
        {
            string key = $"diary~";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    return _decorator.GetAllDiaryEntries();
                });
        }

        public Task<DiaryEntryDTO?> GetDiaryEntryByDate(DateTime date)
        {
            string key = $"diary~{date}";

            return _memoryCache.GetOrCreateAsync(
                key, entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    return _decorator.GetDiaryEntryByDate(date);
                });
        }

        public Task<DiaryEntryDTO?> GetDiaryEntryById(int id)
        {
            string key = $"diary~{id}";

            return _memoryCache.GetOrCreateAsync(
                key, entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    return _decorator.GetDiaryEntryById(id);
                });
        }

        public Task<ICollection<DiaryEntryDTO>?> GetDiaryEntryByUserId(string userId)
        {
            string key = $"diary~{userId}";

            return _memoryCache.GetOrCreateAsync(
                key, entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    return _decorator.GetDiaryEntryByUserId(userId);
                });
        }

        public Task<DiaryEntryDTO?> UpdateDiaryEntry(int entryId, DiaryEntryUpdateDTO entry, string userId)
        {
            return _decorator.UpdateDiaryEntry(entryId, entry, userId);
        }
    }
}

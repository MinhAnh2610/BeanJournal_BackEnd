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
		private readonly ICacheService _cacheService;
		public CachedDiaryEntryService(IDiaryEntryService decorator, ICacheService cacheService)
		{
			_decorator = decorator;
			_cacheService = cacheService;
		}

		public async Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry, string userId)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.AddDiaryEntry(entry, userId);
		}

		public async Task<DiaryEntryDTO?> DeleteDiaryEntry(int id)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.DeleteDiaryEntry(id);
		}

		public async Task<ICollection<DiaryEntryDTO>?> GetAllDiaryEntries()
		{
			return await _cacheService.GetAsync(
				"diaries",
				async () =>
				{
					ICollection<DiaryEntryDTO>? diaries = await _decorator.GetAllDiaryEntries();

					return diaries!;
				});
		}

		public async Task<DiaryEntryDTO?> GetDiaryEntryByDate(DateTime date)
		{
			return await _cacheService.GetAsync(
				$"diary-{date}",
				async () =>
				{
					DiaryEntryDTO? diary = await _decorator.GetDiaryEntryByDate(date);

					return diary!;
				});
		}

		public async Task<DiaryEntryDTO?> GetDiaryEntryById(int id)
		{
			return await _cacheService.GetAsync(
				$"diary-{id}",
				async () =>
				{
					DiaryEntryDTO? diary = await _decorator.GetDiaryEntryById(id);

					return diary!;
				});
		}

		public async Task<ICollection<DiaryEntryDTO>?> GetDiaryEntryByUserId(string userId)
		{
			return await _cacheService.GetAsync(
				$"diary-{userId}",
				async () =>
				{
					ICollection<DiaryEntryDTO>? diary = await _decorator.GetDiaryEntryByUserId(userId);

					return diary!;
				});
		}

		public async Task<DiaryEntryDTO?> UpdateDiaryEntry(int entryId, DiaryEntryUpdateDTO entry, string userId)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.UpdateDiaryEntry(entryId, entry, userId);
		}
	}
}

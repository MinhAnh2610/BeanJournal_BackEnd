using Entities;
using Microsoft.AspNetCore.JsonPatch.Converters;
using Microsoft.VisualBasic;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;
using ServiceContracts.Mapper;

namespace Services
{
	public class DiaryEntryService : IDiaryEntryService
	{
		private readonly IDiaryEntryRepository _entryRepository;
		private readonly IEntryTagRepository _entryTagRepository;
		private readonly ITagRepository _tagRepository;

		public DiaryEntryService(IDiaryEntryRepository entryRepository,
														 IEntryTagRepository entryTagRepository,
														 ITagRepository tagRepository)
		{
			_entryRepository = entryRepository;
			_entryTagRepository = entryTagRepository;
			_tagRepository = tagRepository;
		}
		public async Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry, string userId)
		{
			var entryModel = entry.ToDiaryEntryFromAdd(userId);

			var entryResponse = await _entryRepository.AddDiaryEntryAsync(entryModel);

			if (entry.Tags != null)
			{
				foreach (var tagId in entry.Tags!)
				{
					var tagModel = await _tagRepository.GetTagByIdAsync(tagId);
					if (tagModel != null)
					{
						var entryTagModel = new EntryTag()
						{
							Entry = entryResponse,
							Tag = tagModel
						};
						await _entryTagRepository.AddEntryTagAsync(entryTagModel);
					}
				}
			}

			return entryResponse.ToDiaryEntryDto();
		}

		public async Task<DiaryEntryDTO?> DeleteDiaryEntry(int id)
		{
			var entryResponse = await _entryRepository.DeleteDiaryEntryAsync(id);
			if (entryResponse == null)
			{
				return null;
			}
			await _entryTagRepository.DeleteEntryTagsAsync(id);
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

		public async Task<DiaryEntryDTO?> UpdateDiaryEntry(int entryId, DiaryEntryUpdateDTO entry, string userId)
		{
			var entryModel = entry.ToDiaryEntryFromUpdate(userId);

			var entryResponse = await _entryRepository.UpdateDiaryEntryAsync(entryId, entryModel);
			if (entryResponse == null)
			{
				return null;
			}

			var existingTags = await _entryTagRepository.GetEntryTagByEntryIdAsync(entryId);
			var newTags = entry.Tags ?? new List<int>();

			var tagsToAdd = new List<int>();
			if (existingTags != null)
			{
				var tagsToRemove = existingTags!.Where(x => !newTags.Contains(x.TagId)).ToList();
				foreach (var tagRemove in tagsToRemove)
				{
					await _entryTagRepository.DeleteEntryTagByTagAndEntryAsync(entryId, tagRemove.TagId);
				}
				tagsToAdd = newTags.Where(x => !existingTags!.Any(et => et.TagId == x)).ToList();
			}
			else
			{
				tagsToAdd = newTags.ToList();
			}

			foreach (var tagId in tagsToAdd)
			{
				var entryTagModel = new EntryTag()
				{
					EntryId = entryId,
					TagId = tagId,
				};
				await _entryTagRepository.AddEntryTagAsync(entryTagModel);
			}

			var tags = await _entryTagRepository.GetEntryTagByEntryIdAsync(entryId);
			entryResponse.EntryTags = tags;

			return entryResponse.ToDiaryEntryDto();
		}
	}
}

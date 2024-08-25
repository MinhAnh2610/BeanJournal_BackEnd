using Entities;
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
    private readonly IMediaAttachmentRepository _mediaAttachmentRepository;
    private readonly ITagRepository _tagRepository;
    public DiaryEntryService(IDiaryEntryRepository entryRepository, 
                             IEntryTagRepository entryTagRepository,
                             IMediaAttachmentRepository mediaAttachmentRepository,
                             ITagRepository tagRepository)
    {
      _entryRepository = entryRepository;
      _entryTagRepository = entryTagRepository;
      _mediaAttachmentRepository = mediaAttachmentRepository;
      _tagRepository = tagRepository;
    }
    public async Task<DiaryEntryDTO> AddDiaryEntry(DiaryEntryAddDTO entry, string userId)
    {
      var entryModel = entry.ToDiaryEntryFromAdd(userId);

      var entryResponse = await _entryRepository.AddDiaryEntryAsync(entryModel);

      foreach (var tagId in entry.Tags!)
      {
        var tagModel = await _tagRepository.GetTagByIdAsync(tagId);
        var entryTagModel = new EntryTag()
        {
          Entry = entryModel,
          Tag = tagModel
        };
        await _entryTagRepository.AddEntryTagAsync(entryTagModel);
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

      var tagsToRemove = existingTags!.Where(x => !newTags.Contains(x.TagId)).ToList();
      foreach (var tagRemove in tagsToRemove)
      {
        await _entryTagRepository.DeleteEntryTagByTagAndEntryAsync(entryId, tagRemove.TagId);
      }

      var tagsToAdd = newTags.Where(x => !existingTags!.Any(et => et.TagId == x)).ToList();
      foreach (var tagId in tagsToAdd)
      {
        var entryTagModel = new EntryTag()
        {
          EntryId = entryId,
          TagId = tagId, 
        };
        await _entryTagRepository.AddEntryTagAsync(entryTagModel);
      }

      return entryResponse.ToDiaryEntryDto();
    }
  }
}

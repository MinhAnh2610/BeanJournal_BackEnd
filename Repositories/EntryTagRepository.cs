using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
  public class EntryTagRepository : IEntryTagRepository
  {
    private readonly ApplicationDbContext _context;
    public EntryTagRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<EntryTag> CreateEntryTagAsync(EntryTag entryTag)
    {
      await _context.EntryTags.AddAsync(entryTag);
      await _context.SaveChangesAsync();
      return entryTag;
    }

    public async Task<EntryTag?> DeleteEntryTagAsync(int id)
    {
      var entryTag = await _context.EntryTags.FirstOrDefaultAsync(x => x.TagId == id);
      if (entryTag != null) 
      {
        return null;
      }
      _context.EntryTags.Remove(entryTag);
      await _context.SaveChangesAsync();
      return entryTag;
    }

    public async Task<ICollection<EntryTag>?> GetEntryTagsAsync()
    {
      return await _context.EntryTags.ToListAsync();
    }

    public async Task<ICollection<EntryTag>?> GetEntryTagsByEntriesAsync(DiaryEntry entry)
    {
      return await _context.EntryTags.Where(x => x.EntryId == entry.EntryId).ToListAsync();
    }

    public async Task<ICollection<EntryTag>?> GetEntryTagsByTagsAsync(Tag tag)
    {
      return await _context.EntryTags.Where(x => x.TagId == tag.TagId).ToListAsync();
    }

    public async Task<EntryTag?> UpdateEntryTagAsync(EntryTag entryTag)
    {
      await _context.SaveChangesAsync();
      return entryTag;
    }
  }
}

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
    public async Task<EntryTag> AddEntryTagAsync(EntryTag entryTag)
    {
      await _context.EntryTags.AddAsync(entryTag);  
      await _context.SaveChangesAsync();
      return entryTag;
    }

    public async Task<ICollection<EntryTag>?> DeleteEntryTagsAsync(int entryId)
    {
      var entryTags =await _context.EntryTags.Where(x => x.EntryId == entryId).ToListAsync();
      _context.EntryTags.RemoveRange(entryTags);
      await _context.SaveChangesAsync();
      return entryTags;
    }

    public async Task<EntryTag?> DeleteEntryTagByTagAndEntryAsync(int entryId, int tagId)
    {
      var entryTag = await _context.EntryTags.Where(x => x.EntryId == entryId && x.TagId == tagId).FirstOrDefaultAsync();
      if (entryTag == null)
      {
        return null;
      }
      _context.EntryTags.Remove(entryTag);
      await _context.SaveChangesAsync();
      return entryTag;
    }

    public async Task<ICollection<EntryTag>?> GetEntryTagByEntryIdAsync(int entryId)
    {
      return await _context.EntryTags
        .Include(x => x.Tag)
        .Where(x => x.EntryId == entryId)
        .ToListAsync();
    }
  }
}

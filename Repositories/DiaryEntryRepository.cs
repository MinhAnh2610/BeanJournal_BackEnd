using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RepositoryContracts;
using System.Security.Cryptography.X509Certificates;

namespace Repositories
{
  public class DiaryEntryRepository : IDiaryEntryRepository
  {
    private readonly ApplicationDbContext _context;
    public DiaryEntryRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<DiaryEntry> AddDiaryEntryAsync(DiaryEntry entry)
    {
      await _context.DiaryEntries.AddAsync(entry);
      await _context.SaveChangesAsync();
      return entry;
    }

    public async Task<DiaryEntry?> DeleteDiaryEntryAsync(int id)
    {
      var entry = await _context.DiaryEntries.FindAsync(id);
      if (entry == null)
      {
        return null;
      }
      _context.DiaryEntries.Remove(entry);
      await _context.SaveChangesAsync();
      return entry;
    }

    public async Task<ICollection<DiaryEntry>?> GetDiaryEntriesAsync()
    {
      return await _context.DiaryEntries.Include(x => x.User).ToListAsync();
    }

    public async Task<ICollection<DiaryEntry>?> GetDiaryEntriesByUserAsync(string id)
    {
      return await _context.DiaryEntries.Include(x => x.User).Where(x => x.UserId == id).ToListAsync();
    }

    public async Task<DiaryEntry?> GetDiaryEntryByDateAsync(DateTime date)
    {
      return await _context.DiaryEntries.Include(x => x.User).FirstOrDefaultAsync(x => x.CreatedAt.Date == date);
    }

    public async Task<DiaryEntry?> GetDiaryEntryByIdAsync(int id)
    {
      return await _context.DiaryEntries.Include(x => x.User).FirstOrDefaultAsync(x => x.EntryId == id);
    }

    public async Task<DiaryEntry?> UpdateDiaryEntryAsync(DiaryEntry entry)
    {
      var existingEntry = await _context.DiaryEntries.FindAsync(entry.EntryId);
      if (existingEntry == null)
      {
        return null;
      }
      existingEntry.Content = entry.Content;
      existingEntry.Title = entry.Title;
      existingEntry.Mood = entry.Mood;
      existingEntry.UpdatedAt = entry.UpdatedAt;

      await _context.SaveChangesAsync();
      return existingEntry;
    }
  }
}

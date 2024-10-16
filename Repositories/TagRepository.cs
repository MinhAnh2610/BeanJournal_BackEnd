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
	public class TagRepository : ITagRepository
  {
    private readonly ApplicationDbContext _context;
    public TagRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<Tag> CreateTagAsync(Tag tag)
    {
      await _context.Tags.AddAsync(tag);
      await _context.SaveChangesAsync();
      return tag;
    }

    public async Task<Tag?> DeleteTagAsync(int id)
    {
      var tag = await _context.Tags.FindAsync(id);
      if (tag == null) 
      {
        return null;
      }
      _context.Tags.Remove(tag);
      await _context.SaveChangesAsync();
      return tag;
    }

    public async Task<Tag?> GetTagByIdAsync(int id)
    {
      return await _context.Tags.FindAsync(id);
    }

		public async Task<Tag?> GetTagByNameAsync(string name)
		{
			return await _context.Tags.FirstAsync(x => x.Name == name);
		}

		public async Task<ICollection<Tag>?> GetTagsAsync()
    {
      return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> UpdateTagAsync(int tagId, Tag tag)
    {
      var existingTag = await _context.Tags.FindAsync(tagId);
      if (existingTag == null) 
      {
        return null;
      }
      existingTag.Name = tag.Name;
      existingTag.IconPublicId = tag.IconPublicId;
      existingTag.ImagePublicId = tag.ImagePublicId;
      existingTag.IconUrl = tag.IconUrl;
      existingTag.ImageUrl = tag.ImageUrl;

      await _context.SaveChangesAsync();
      return existingTag;
    }
  }
}

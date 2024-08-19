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
  public class MediaAttachmentRepository : IMediaAttachmentRepository
  {
    private readonly ApplicationDbContext _context;
    public MediaAttachmentRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<MediaAttachment> CreateMediaAttachmentAsync(MediaAttachment mediaAttachment)
    {
      await _context.MediaAttachments.AddAsync(mediaAttachment);
      await _context.SaveChangesAsync();
      return mediaAttachment;
    }

    public async Task<MediaAttachment?> DeleteMediaAttachmentAsync(int id)
    {
      var media = await _context.MediaAttachments.FindAsync(id);
      if (media == null) 
      {
        return null;
      }
      _context.MediaAttachments.Remove(media);
      await _context.SaveChangesAsync();
      return media;
    }

    public async Task<MediaAttachment?> GetMediaAttachmentByIdAsync(int id)
    {
      return await _context.MediaAttachments.FindAsync(id);
    }

    public async Task<ICollection<MediaAttachment>?> GetMediaAttachmentsAsync()
    {
      return await _context.MediaAttachments.ToListAsync();
    }

    public async Task<MediaAttachment?> UpdateMediaAttachmentAsync(MediaAttachment mediaAttachment)
    {
      var existingMedia = await _context.MediaAttachments.FindAsync(mediaAttachment.MediaId);
      if (existingMedia == null) 
      {
        return null;
      }
      existingMedia.FilePath = mediaAttachment.FilePath;
      existingMedia.FileType = mediaAttachment.FileType;  
      
      await _context.SaveChangesAsync();
      return existingMedia;
    }
  }
}

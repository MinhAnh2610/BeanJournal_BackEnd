using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO.Tag;
using ServiceContracts.DTO.MediaAttachment;

namespace ServiceContracts.DTO.DiaryEntry
{
  public class DiaryEntryDTO
  {
    public int EntryId { get; set; }
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    [StringLength(20)]
    public string Mood { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<TagDTO>? Tags { get; set; }
    public ICollection<MediaAttachmentDTO>? MediaAttachments { get; set; }
  }
}

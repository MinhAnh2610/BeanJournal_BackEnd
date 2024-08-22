using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO.Tag;

namespace ServiceContracts.DTO.DiaryEntry
{
  public class DiaryEntryDTO
  {
    public int EntryId { get; set; }
    public string Username { get; set; } = string.Empty;
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    [StringLength(20)]
    public string Mood { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<TagDTO>? Tags { get; set; }
  }
}

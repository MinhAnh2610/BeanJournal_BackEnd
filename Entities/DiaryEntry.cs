using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  [Table("DiaryEntries")]
  public class DiaryEntry
  {
    [Key]
    public int DiaryEntryId { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    [StringLength(20)]
    public string Mood { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public ICollection<EntryTag>? EntryTags { get; set; }
    public ICollection<MediaAttachment>? MediaAttachments { get; set; }
  }
}

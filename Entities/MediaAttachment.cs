using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  [Table("MediaAttachments")]
  public class MediaAttachment
  {
    [Key]
    public int MediaId { get; set; }
    public int EntryId { get; set; }
    [ForeignKey("EntryId")]
    public DiaryEntry? Entry { get; set; }
    [StringLength(255)]
    public string FilePath { get; set; } = string.Empty;
    [StringLength(50)]
    public string FileType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
  }
}

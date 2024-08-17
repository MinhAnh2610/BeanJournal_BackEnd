using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  [Table("EntryTags")]
  public class EntryTag
  {
    [Key]
    public int EntryTagId { get; set; }
    public int EntryId { get; set; }
    [ForeignKey("EntryId")]
    public DiaryEntry? Entry { get; set; }
    public int TagId { get; set; }
    [ForeignKey("TagId")]
    public Tag? Tag { get; set; }
  }
}

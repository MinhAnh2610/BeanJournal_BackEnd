using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  [Table("Tags")]
  public class Tag
  {
    [Key]
    public int TagId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public ICollection<EntryTag>? EntryTags { get; set; }
  }
}

using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.DiaryEntry
{
  public class DiaryEntryUpdateDTO
  {
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    [StringLength(20)]
    public string Mood { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<int>? Tags { get; set; }
  }
}

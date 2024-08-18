using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
  [Table("Users")]
  public class User : IdentityUser
  {
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<DiaryEntry>? DiaryEntries { get; set; }
  }
}

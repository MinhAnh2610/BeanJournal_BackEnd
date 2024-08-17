using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
  [Table("Users")]
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [StringLength(50)]
    public string UserName { get; set; } = string.Empty;
    [StringLength(50)]
    public string Password { get; set; } = string.Empty;
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<DiaryEntry>? DiaryEntries { get; set; }
  }
}

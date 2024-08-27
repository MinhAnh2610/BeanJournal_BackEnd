using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
  [Table("Users")]
  public class ApplicationUser : IdentityUser
  {
    public string ProfileImagePublicId { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpirationDateTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<DiaryEntry>? DiaryEntries { get; set; }
  }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<DiaryEntry> DiaryEntries { get; set; }
    public DbSet<EntryTag> EntryTags { get; set; }
    public DbSet<MediaAttachment> MediaAttachments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Define composite keys
      modelBuilder.Entity<EntryTag>()
        .HasKey(et => new { et.EntryId, et.TagId });

      modelBuilder.Entity<EntryTag>()
        .HasOne(et => et.Entry)
        .WithMany(d => d.EntryTags)
        .HasForeignKey(et => et.EntryId);

      modelBuilder.Entity<EntryTag>()
        .HasOne(et => et.Tag)
        .WithMany(t => t.EntryTags)
        .HasForeignKey(et => et.TagId);

      #region Seed Data
      string usersJson = System.IO.File.ReadAllText("SampleData/users.json");
      List<User>? users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson);
      modelBuilder.Entity<User>().HasData(users!);

      string rolesJson = System.IO.File.ReadAllText("SampleData/roles.json");
      List<Role>? roles = System.Text.Json.JsonSerializer.Deserialize<List<Role>>(rolesJson);
      modelBuilder.Entity<Role>().HasData(roles!);

      string userRolesJson = System.IO.File.ReadAllText("SampleData/userRoles.json");
      List<IdentityUserRole<string>>? userRoles = System.Text.Json.JsonSerializer.Deserialize<List<IdentityUserRole<string>>>(userRolesJson);
      modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles!);

      string diaryEntriesJson = System.IO.File.ReadAllText("SampleData/diaryEntries.json");
      List<DiaryEntry>? diaryEntries = System.Text.Json.JsonSerializer.Deserialize<List<DiaryEntry>>(diaryEntriesJson);
      modelBuilder.Entity<DiaryEntry>().HasData(diaryEntries!);

      string tagsJson = System.IO.File.ReadAllText("SampleData/tags.json");
      List<Tag>? tags = System.Text.Json.JsonSerializer.Deserialize<List<Tag>>(tagsJson);
      modelBuilder.Entity<Tag>().HasData(tags!);

      string entryTagsJson = System.IO.File.ReadAllText("SampleData/entryTags.json");
      List<EntryTag>? entryTags = System.Text.Json.JsonSerializer.Deserialize<List<EntryTag>>(entryTagsJson);
      modelBuilder.Entity<EntryTag>().HasData(entryTags!);

      string mediaAttachmentsJson = System.IO.File.ReadAllText("SampleData/mediaAttachments.json");
      List<MediaAttachment>? mediaAttachments = System.Text.Json.JsonSerializer.Deserialize<List<MediaAttachment>>(mediaAttachmentsJson);
      modelBuilder.Entity<MediaAttachment>().HasData(mediaAttachments!);
      #endregion
    }
  }
}

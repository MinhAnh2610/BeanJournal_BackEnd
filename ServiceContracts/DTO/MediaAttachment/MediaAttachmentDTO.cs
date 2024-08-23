using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.MediaAttachment
{
  public class MediaAttachmentDTO
  {
    public int MediaId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string EntryTitle { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
  }
}

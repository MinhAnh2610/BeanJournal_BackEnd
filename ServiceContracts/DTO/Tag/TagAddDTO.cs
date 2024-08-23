using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Tag
{
  public class TagAddDTO
  {
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
  }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
  public interface ITagRepository
  {
    Task<Tag> CreateTagAsync(Tag tag);
    Task<ICollection<Tag>?> GetTagsAsync();
    Task<Tag?> GetTagByIdAsync(int id);
		Task<Tag?> GetTagByNameAsync(string name);
    Task<Tag?> UpdateTagAsync(int tagId, Tag tag);
    Task<Tag?> DeleteTagAsync(int id);
  }
}

using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Caching
{
    public class CachedTagService : ITagService
    {
        private readonly ITagService _decorator;
        private readonly IMemoryCache _memoryCache;
        public CachedTagService(ITagService decorator, IMemoryCache memoryCache)
        {
            _decorator = decorator;
            _memoryCache = memoryCache;
        }
        public Task<TagDTO> AddTag(TagAddDTO tag)
        {
            return _decorator.AddTag(tag);
        }

        public Task<DeletionResult> DeleteIcon(string publicId)
        {
            return _decorator.DeleteIcon(publicId);
        }

        public Task<DeletionResult> DeleteImage(string publicId)
        {
            return _decorator.DeleteImage(publicId);
        }

        public Task<TagDTO?> DeleteTag(int id)
        {
            return _decorator.DeleteTag(id);
        }

        public Task<ICollection<TagDTO>?> GetAllTags()
        {
            string key = $"tag~";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    return _decorator.GetAllTags();
                });
        }

        public Task<TagDTO?> GetTagById(int id)
        {
            string key = $"tag~{id}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                return _decorator.GetTagById(id);
            });
        }

        public Task<TagDTO?> UpdateTag(int tagId, TagAddDTO tag)
        {
            return _decorator.UpdateTag(tagId, tag);
        }

        public Task<ImageUploadResult> UploadIcon(TagAddDTO tag)
        {
            return _decorator.UploadIcon(tag);
        }

        public Task<ImageUploadResult> UploadImage(TagAddDTO tag)
        {
            return _decorator.UploadImage(tag);
        }
    }
}

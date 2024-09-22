using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly IDistributedCache _distributedCache;
        public CachedTagService(ITagService decorator, IDistributedCache distributedCache)
        {
            _decorator = decorator;
            _distributedCache = distributedCache;
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

        public async Task<ICollection<TagDTO>?> GetAllTags()
        {
            string key = $"tag~";

            string? cachedTags = await _distributedCache.GetStringAsync(key);

            ICollection<TagDTO>? tags;

            if (string.IsNullOrEmpty(cachedTags))
            {
                tags = await _decorator.GetAllTags();

                if (tags is null)
                {
                    return tags;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(tags));
            }

            tags = JsonConvert.DeserializeObject<ICollection<TagDTO>?>
                (cachedTags!,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                });

            return tags;
        }

        public async Task<TagDTO?> GetTagById(int id)
        {
            string key = $"tag~{id}";

            string? cachedTags = await _distributedCache.GetStringAsync(key);

            TagDTO? tag;

            if (string.IsNullOrEmpty(cachedTags))
            {
                tag = await _decorator.GetTagById(id);

                if (tag is null)
                {
                    return tag;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(tag));
            }

            tag = JsonConvert.DeserializeObject<TagDTO?>(cachedTags!);

            return tag;
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

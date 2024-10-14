using CloudinaryDotNet.Actions;
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
		private readonly ICacheService _cacheService;
		public CachedTagService(ITagService decorator, ICacheService service)
		{
			_decorator = decorator;
			_cacheService = service;
		}
		public async Task<TagDTO> AddTag(TagAddDTO tag)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");
			await _cacheService.RemoveByPrefixAsync("tag");

			return await _decorator.AddTag(tag);
		}

		public Task<DeletionResult> DeleteIcon(string publicId)
		{
			return _decorator.DeleteIcon(publicId);
		}

		public Task<DeletionResult> DeleteImage(string publicId)
		{
			return _decorator.DeleteImage(publicId);
		}

		public async Task<TagDTO?> DeleteTag(int id)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");
			await _cacheService.RemoveByPrefixAsync("tag");

			return await _decorator.DeleteTag(id);
		}

		public async Task<ICollection<TagDTO>?> GetAllTags()
		{
			return await _cacheService.GetAsync(
				"tags",
				async () =>
				{
					ICollection<TagDTO>? tags = await _decorator.GetAllTags();

					return tags!;
				});
		}

		public async Task<TagDTO?> GetTagById(int id)
		{
			return await _cacheService.GetAsync(
				$"tag-{id}",
				async () =>
				{
					TagDTO? tag = await _decorator.GetTagById(id);

					return tag!;
				});
		}

		public async Task<TagDTO?> UpdateTag(int tagId, TagAddDTO tag)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");
			await _cacheService.RemoveByPrefixAsync("tag");

			return await _decorator.UpdateTag(tagId, tag);
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

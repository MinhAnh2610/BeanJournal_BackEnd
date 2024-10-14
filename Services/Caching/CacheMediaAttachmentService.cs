using CloudinaryDotNet.Actions;
using ServiceContracts;
using ServiceContracts.DTO.MediaAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Caching
{
	public class CacheMediaAttachmentService : IMediaAttachmentService
	{
		private readonly IMediaAttachmentService _decorator;
		private readonly ICacheService _cacheService;
		public CacheMediaAttachmentService(IMediaAttachmentService decorator, ICacheService cacheService)
		{
			_decorator = decorator;
			_cacheService = cacheService;
		}
		public async Task<ICollection<MediaAttachmentDTO>?> AddMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentAddDtos, int entryId)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.AddMediaAttachment(mediaAttachmentAddDtos, entryId);
		}

		public Task<DeletionResult> DeleteImage(string publicId)
		{
			return _decorator.DeleteImage(publicId);
		}

		public async Task<MediaAttachmentDTO?> DeleteMediaAttachment(int id)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.DeleteMediaAttachment(id);
		}

		public Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachments()
		{
			return _decorator.GetAllMediaAttachments();
		}

		public Task<ICollection<MediaAttachmentDTO>?> GetAllMediaAttachmentsByUser(string userId)
		{
			return _decorator.GetAllMediaAttachmentsByUser(userId);
		}

		public Task<MediaAttachmentDTO?> GetMediaAttachmentById(int id)
		{
			return _decorator.GetMediaAttachmentById(id);
		}

		public async Task<ICollection<MediaAttachmentDTO>?> UpdateMediaAttachment(List<MediaAttachmentAddDTO> mediaAttachmentUpdateDTOs, int entryId)
		{
			await _cacheService.RemoveAsync("diaries");
			await _cacheService.RemoveByPrefixAsync("diary");

			return await _decorator.UpdateMediaAttachment(mediaAttachmentUpdateDTOs, entryId);
		}

		public Task<ImageUploadResult> UploadImage(MediaAttachmentAddDTO mediaAttachment)
		{
			return _decorator.UploadImage(mediaAttachment);
		}
	}
}

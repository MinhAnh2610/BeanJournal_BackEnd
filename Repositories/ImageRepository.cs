using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly IOptions<CloudinarySettings> _config;
		private readonly Cloudinary _cloudinary;
		public ImageRepository(IOptions<CloudinarySettings> config)
		{
			var acc = new Account()
			{
				Cloud = config.Value.CloudName,
				ApiKey = config.Value.ApiKey,
				ApiSecret = config.Value.ApiSecret
			};

			_config = config;

			_cloudinary = new Cloudinary(acc);
		}
		public async Task<DeletionResult> DeleteByPublicId(string publicId)
		{
			var deleteParams = new DeletionParams(publicId);
			var result = await _cloudinary.DestroyAsync(deleteParams);

			return result;
		}

		public async Task<ImageUploadResult> UploadImage(IFormFile image, int height, int width)
		{
			var uploadResult = new ImageUploadResult();
			if (image.Length > 0)
			{
				using var stream = image.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(image.FileName, stream),
					Transformation = new Transformation().Height(height).Width(width).Crop("fill").Gravity("face")
				};
				uploadResult = await _cloudinary.UploadAsync(uploadParams);
			}
			return uploadResult;
		}
	}
}

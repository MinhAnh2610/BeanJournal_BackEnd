using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
	public interface IImageRepository
	{
		Task<DeletionResult> DeleteByPublicId(string publicId);
		Task<ImageUploadResult> UploadImage(IFormFile image, int height, int width);
	}
}

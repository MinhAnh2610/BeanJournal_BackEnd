using AutoFixture;
using CloudinaryDotNet.Actions;
using Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.MediaAttachment;
using ServiceContracts.Mapper;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanJournal_BackEnd.Tests.Unit.Services
{
	public class MediaAttachmentServiceTest
	{
		private readonly IMediaAttachmentService _mediaAttachmentService;
		private readonly IMediaAttachmentRepository _mediaAttachmentRepositoryMock;
		private readonly IImageRepository _imageRepositoryMock;

		private readonly IFixture _fixture;

		public MediaAttachmentServiceTest()
		{
			_fixture = new Fixture();

			_mediaAttachmentRepositoryMock = Substitute.For<IMediaAttachmentRepository>();
			_imageRepositoryMock = Substitute.For<IImageRepository>();

			_mediaAttachmentService = new MediaAttachmentService(_mediaAttachmentRepositoryMock, _imageRepositoryMock);
		}

		#region AddMediaAttachment
		[Fact]
		public async Task AddMediaAttachment_InvalidMediaExtension_ShouldReturnNull()
		{
			// Arrange
			var entryId = 1;
			var media_attachments_list = new List<MediaAttachmentAddDTO>()
		{
				new MediaAttachmentAddDTO { File = Substitute.For<IFormFile>() },
				new MediaAttachmentAddDTO { File = Substitute.For<IFormFile>() }
		};

			var image_upload_result = new ImageUploadResult()
			{
				PublicId = "test_image_public_id",
				Url = new Uri("https://test_image_url")
			};
			var media_attachment_model = _fixture.Build<MediaAttachment>()
				.With(t => t.EntryId, entryId)
				.With(t => t.Entry, null as DiaryEntry)
				.Create();

			_imageRepositoryMock.UploadImage(Arg.Any<IFormFile>(), 500, 500).Returns(image_upload_result);
			_mediaAttachmentRepositoryMock.CreateMediaAttachmentAsync(Arg.Any<MediaAttachment>()).Returns(media_attachment_model);

			// Act
			var actual_media_response = await _mediaAttachmentService.AddMediaAttachment(media_attachments_list, entryId);

			// Assert
			actual_media_response.Should().BeNull();
			await _imageRepositoryMock.DidNotReceive().UploadImage(Arg.Any<IFormFile>(), 500, 500);
			await _mediaAttachmentRepositoryMock.DidNotReceive().CreateMediaAttachmentAsync(Arg.Any<MediaAttachment>());
		}

		[Fact]
		public async Task AddMediaAttachment_EmptyMediaList_ShouldReturnEmptyList()
		{
			// Arrange
			var entryId = 1;
			var media_attachments_list = new List<MediaAttachmentAddDTO>();

			// Act
			var actual_media_response = await _mediaAttachmentService.AddMediaAttachment(media_attachments_list, entryId);

			// Assert
			actual_media_response.Should().BeEmpty();
			actual_media_response.Should().NotBeNull();
			await _imageRepositoryMock.DidNotReceive().UploadImage(Arg.Any<IFormFile>(), 500, 500);
			await _mediaAttachmentRepositoryMock.DidNotReceive().CreateMediaAttachmentAsync(Arg.Any<MediaAttachment>());
		}
		#endregion

		#region UpdateMediaAttachment
		[Fact]
		public async Task UpdateMediaAttachment_InvalidMediaExtension_ShouldReturnNull()
		{
			// Arrange
			var entryId = 1;
			var media_attachments_list = new List<MediaAttachmentAddDTO>
		{
				new MediaAttachmentAddDTO { File = Substitute.For<IFormFile>() }
		};

			// Act
			var actual_media_response = await _mediaAttachmentService.UpdateMediaAttachment(media_attachments_list, entryId);

			// Assert
			actual_media_response.Should().BeNull();
			await _imageRepositoryMock.DidNotReceive().UploadImage(Arg.Any<IFormFile>(), 500, 500);
			await _mediaAttachmentRepositoryMock.DidNotReceive().CreateMediaAttachmentAsync(Arg.Any<MediaAttachment>());
		}

		[Fact]
		public async Task UpdateMediaAttachment_EmptyMediaList_ShouldReturnEmptyList()
		{
			// Arrange
			var entryId = 1;
			var media_attachments_update_list = new List<MediaAttachmentAddDTO>();

			// Act
			var actual_media_response = await _mediaAttachmentService.UpdateMediaAttachment(media_attachments_update_list, entryId);

			// Assert
			actual_media_response.Should().NotBeNull();
			actual_media_response.Should().BeEmpty();
			await _imageRepositoryMock.DidNotReceive().UploadImage(Arg.Any<IFormFile>(), 500, 500);
			await _mediaAttachmentRepositoryMock.DidNotReceive().CreateMediaAttachmentAsync(Arg.Any<MediaAttachment>());
		}
		#endregion

		#region GetAllMediaAttachments
		[Fact]
		public async Task GetAllMediaAttachments_RepositoryReturnsMediaAttachments_ShouldReturnMediaAttachmentDTOs()
		{
			// Arrange
			var media_attachments_list = new List<MediaAttachment>
		{
				new MediaAttachment { MediaId = 1 },
				new MediaAttachment { MediaId = 2 }
		};

			_mediaAttachmentRepositoryMock.GetMediaAttachmentsAsync().Returns(media_attachments_list);

			// Act
			var actual_media_response = await _mediaAttachmentService.GetAllMediaAttachments();

			// Assert
			actual_media_response.Should().NotBeNull().And.HaveCount(2);
			await _mediaAttachmentRepositoryMock.Received(1).GetMediaAttachmentsAsync();
		}

		[Fact]
		public async Task GetAllMediaAttachments_RepositoryReturnsNull_ShouldReturnNull()
		{
			// Arrange
			_mediaAttachmentRepositoryMock.GetMediaAttachmentsAsync().Returns((ICollection<MediaAttachment>?)null);

			// Act
			var actual_media_response = await _mediaAttachmentService.GetAllMediaAttachments();

			// Assert
			actual_media_response.Should().BeNull();
			await _mediaAttachmentRepositoryMock.Received(1).GetMediaAttachmentsAsync();
		}
		#endregion

		#region GetMediaAttachmentById
		[Fact]
		public async Task GetMediaAttachmentById_RepositoryReturnsMediaAttachment_ShouldReturnMediaAttachmentDTO()
		{
			// Arrange
			var media_attachment = _fixture.Build<MediaAttachment>()
				.With(t => t.Entry, null as DiaryEntry).Create();

			_mediaAttachmentRepositoryMock
					.GetMediaAttachmentByIdAsync(1)
					.Returns(media_attachment);  // Mock repository returning a media attachment

			var expected_media_response = media_attachment.ToMediaAttachmentDto();

			// Act
			var actual_media_response = await _mediaAttachmentService.GetMediaAttachmentById(1);

			// Assert
			actual_media_response.Should().NotBeNull();
			actual_media_response.Should().BeEquivalentTo(expected_media_response);
			await _mediaAttachmentRepositoryMock.Received(1).GetMediaAttachmentByIdAsync(1);  
		}

		[Fact]	
		public async Task GetMediaAttachmentById_RepositoryReturnsNull_ShouldReturnNull()
		{
			// Arrange
			_mediaAttachmentRepositoryMock
					.GetMediaAttachmentByIdAsync(Arg.Any<int>())
					.Returns((MediaAttachment?)null);  

			// Act
			var actual_media_response = await _mediaAttachmentService.GetMediaAttachmentById(1);

			// Assert
			actual_media_response.Should().BeNull();
			await _mediaAttachmentRepositoryMock.Received(1).GetMediaAttachmentByIdAsync(1); 
		}
		#endregion

		#region GetAllMediaAttachmentsByUser

		#endregion

		#region DeleteMediaAttachment

		#endregion
	}
}

using AutoFixture;
using CloudinaryDotNet.Actions;
using Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Tag;
using ServiceContracts.Mapper;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanJournal_BackEnd.Tests.Unit.Services
{
	public class TagServiceTest
	{
		private readonly ITagService _tagService;
		private readonly ITagRepository _tagRepositoryMock;
		private readonly IImageRepository _imageRepositoryMock;

		private readonly IFixture _fixture;

		public TagServiceTest()
		{
			_fixture = new Fixture();
			_tagRepositoryMock = Substitute.For<ITagRepository>();
			_imageRepositoryMock = Substitute.For<IImageRepository>();

			_tagService = new TagService(_tagRepositoryMock, _imageRepositoryMock);
		}

		#region AddTag
		[Fact]
		public async Task AddTag_ValidTag_ShouldCreateTagAndReturnTagDTO()
		{
			//Arrange
			IFormFile mockImageFile = Substitute.For<IFormFile>();

			TagAddDTO tag_add_request = _fixture
				.Build<TagAddDTO>()
				.With(temp => temp.Name, "Nature")
				.With(temp => temp.Image, mockImageFile)
				.With(temp => temp.Icon, mockImageFile)
				.Create();

			ImageUploadResult tag_image_result = new ImageUploadResult()
			{
				PublicId = "test_image_public_id",
				Url = new Uri("https://test_image_url")
			};
			ImageUploadResult tag_icon_result = new ImageUploadResult()
			{
				PublicId = "test_icon_public_id",
				Url = new Uri("https://test_icon_url")
			};

			Tag expected_tag_response = tag_add_request.ToTagFromAdd(tag_image_result, tag_icon_result);

			_imageRepositoryMock.UploadImage(mockImageFile, 500, 500).Returns(tag_image_result);
			_imageRepositoryMock.UploadImage(mockImageFile, 100, 100).Returns(tag_icon_result);

			_tagRepositoryMock.CreateTagAsync(Arg.Any<Tag>()).Returns(expected_tag_response);
			_tagRepositoryMock.GetTagByNameAsync(Arg.Any<string>()).Returns((Tag?)null);

			//Act
			TagDTO actual_tag_response = await _tagService.AddTag(tag_add_request);

			//Assert
			actual_tag_response.Should().NotBeNull();
			actual_tag_response.Name.Should().BeEquivalentTo(expected_tag_response.Name);
			actual_tag_response.Should().BeOfType<TagDTO>();
		}

		[Fact]
		public async Task AddTag_ValidTag_ShouldCallRepositoryOnce()
		{
			//Arrange
			IFormFile mockImageFile = Substitute.For<IFormFile>();

			TagAddDTO tag_add_request = _fixture
				.Build<TagAddDTO>()
				.With(temp => temp.Name, "Nature")
				.With(temp => temp.Image, mockImageFile)
				.With(temp => temp.Icon, mockImageFile)
				.Create();

			ImageUploadResult tag_image_result = new ImageUploadResult()
			{
				PublicId = "test_image_public_id",
				Url = new Uri("https://test_image_url")
			};
			ImageUploadResult tag_icon_result = new ImageUploadResult()
			{
				PublicId = "test_icon_public_id",
				Url = new Uri("https://test_icon_url")
			};

			Tag expected_tag_response = tag_add_request.ToTagFromAdd(tag_image_result, tag_icon_result);

			_imageRepositoryMock.UploadImage(mockImageFile, 500, 500).Returns(tag_image_result);
			_imageRepositoryMock.UploadImage(mockImageFile, 100, 100).Returns(tag_icon_result);

			_tagRepositoryMock.CreateTagAsync(Arg.Any<Tag>()).Returns(expected_tag_response);
			_tagRepositoryMock.GetTagByNameAsync(Arg.Any<string>()).Returns((Tag?)null);

			//Act
			await _tagService.AddTag(tag_add_request);

			//Assert
			await _tagRepositoryMock.Received(1).CreateTagAsync(Arg.Any<Tag>());
		}

		[Fact]
		public async Task AddTag_DuplicateName_ShouldThrowArgumentException()
		{
			//Arrange
			IFormFile mockImageFile = Substitute.For<IFormFile>();

			TagAddDTO first_tag_request = _fixture
				.Build<TagAddDTO>()
				.With(temp => temp.Name, "Nature")
				.With(temp => temp.Image, mockImageFile)
				.With(temp => temp.Icon, mockImageFile)
				.Create();

			TagAddDTO second_tag_request = _fixture
				.Build<TagAddDTO>()
				.With(temp => temp.Name, "Nature")
				.With(temp => temp.Image, mockImageFile)
				.With(temp => temp.Icon, mockImageFile)
				.Create();

			ImageUploadResult first_tag_image_result = new ImageUploadResult()
			{
				PublicId = "test_image_public_id",
				Url = new Uri("https://test_image_url")
			};
			ImageUploadResult first_tag_icon_result = new ImageUploadResult()
			{
				PublicId = "test_icon_public_id",
				Url = new Uri("https://test_icon_url")
			};

			ImageUploadResult second_tag_image_result = new ImageUploadResult()
			{
				PublicId = "test_image_public_id",
				Url = new Uri("https://test_image_url")
			};
			ImageUploadResult second_tag_icon_result = new ImageUploadResult()
			{
				PublicId = "test_icon_public_id",
				Url = new Uri("https://test_icon_url")
			};

			_imageRepositoryMock.UploadImage(mockImageFile, 500, 500).Returns(first_tag_image_result);
			_imageRepositoryMock.UploadImage(mockImageFile, 100, 100).Returns(first_tag_icon_result);

			Tag first_tag = first_tag_request
				.ToTagFromAdd(first_tag_image_result, first_tag_icon_result);

			Tag second_tag = second_tag_request
				.ToTagFromAdd(second_tag_image_result, second_tag_icon_result);

			_tagRepositoryMock.CreateTagAsync(Arg.Any<Tag>()).ReturnsForAnyArgs(first_tag);
			_tagRepositoryMock.GetTagByNameAsync(first_tag_request.Name).Returns(null as Tag);

			TagDTO first_tag_from_add_tag = await _tagService.AddTag(first_tag_request);

			//Act
			var action = async () =>
			{
				_tagRepositoryMock.CreateTagAsync(Arg.Any<Tag>()).Returns(second_tag);
				_tagRepositoryMock.GetTagByNameAsync(second_tag_request.Name).Returns(first_tag);

				await _tagService.AddTag(second_tag_request);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentException>();
		}
		#endregion

		#region GetAllTags
		[Fact]
		public async Task GetAllTags_RepositoryReturnsNull_ShouldReturnNull()
		{
			//Arrange
			_tagRepositoryMock.GetTagsAsync().Returns(null as ICollection<Tag>);

			//Act
			var actual_tags_response = await _tagService.GetAllTags();

			//Assert
			actual_tags_response.Should().BeNull();
		}

		[Fact]
		public async Task GetAllTags_RepositoryReturnsTags_ShouldReturnListOfTagDTOs()
		{
			//Arrage
			ICollection<Tag> tags = _fixture
				.Build<Tag>()
				.With(temp => temp.EntryTags, null as ICollection<EntryTag>)
				.CreateMany(3)
				.ToList();

			_tagRepositoryMock.GetTagsAsync().Returns(tags);

			ICollection<TagDTO> expected_tags_response = tags.Select(x => x.ToTagDto()).ToList();

			//Act
			ICollection<TagDTO>? actual_tags_response = await _tagService.GetAllTags();

			//Assert
			actual_tags_response.Should().NotBeEmpty();
			actual_tags_response.Should().BeEquivalentTo(expected_tags_response);
		}

		[Fact]
		public async Task GetAllTags_RepositoryReturnsEmptyList_ShouldReturnEmptyList()
		{
			//Arrange
			ICollection<Tag> tags = new List<Tag>();

			_tagRepositoryMock.GetTagsAsync().Returns(tags);

			ICollection<TagAddDTO> expected_tags_reponse = new List<TagAddDTO>();

			//Act
			ICollection<TagDTO>? actual_tags_response = await _tagService.GetAllTags();

			//Assert
			actual_tags_response.Should().NotBeNull();
			actual_tags_response.Should().BeEmpty();
			actual_tags_response.Should().BeEquivalentTo(expected_tags_reponse);
		}
		#endregion

		#region GetTagById
		[Fact]
		public async Task GetTagById_TagExist_ShouldReturnTagDTO()
		{
			//Arrange
			Tag tag = _fixture.Build<Tag>()
				.With(temp => temp.TagId, 1)
				.With(temp => temp.EntryTags, null as ICollection<EntryTag>)
				.Create();

			_tagRepositoryMock.GetTagByIdAsync(tag.TagId).Returns(tag);

			TagDTO expected_tag_response = tag.ToTagDto();

			//Act
			TagDTO? actual_tag_response = await _tagService.GetTagById(tag.TagId);

			//Assert
			actual_tag_response.Should().NotBeNull();
			actual_tag_response.Should().BeEquivalentTo(expected_tag_response);
		}

		[Fact]
		public async Task GetTagById_TagDoesNotExist_ShouldReturnNull()
		{ 
		
		}

		[Fact]
		public async Task GetTagById_InvalidId_ShouldReturnNull()
		{ 
			
		}
		#endregion

		#region UpdateTag

		#endregion

		#region DeleteTag

		#endregion
	}
}

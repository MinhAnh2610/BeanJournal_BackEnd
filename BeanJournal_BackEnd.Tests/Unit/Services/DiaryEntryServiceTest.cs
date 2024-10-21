using AutoFixture;
using Entities;
using FluentAssertions;
using NSubstitute;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;
using ServiceContracts.Mapper;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanJournal_BackEnd.Tests.Unit.Services
{
	public class DiaryEntryServiceTest
	{
		private readonly IDiaryEntryService _diaryEntryService;

		private readonly IDiaryEntryRepository _entryRepositoryMock;
		private readonly IEntryTagRepository _entryTagRepositoryMock;
		private readonly ITagRepository _tagRepositoryMock;

		private readonly IFixture _fixture;

		public DiaryEntryServiceTest()
		{
			_entryRepositoryMock = Substitute.For<IDiaryEntryRepository>();
			_entryTagRepositoryMock = Substitute.For<IEntryTagRepository>();
			_tagRepositoryMock = Substitute.For<ITagRepository>();

			_fixture = new Fixture();

			_diaryEntryService = new DiaryEntryService(_entryRepositoryMock, _entryTagRepositoryMock, _tagRepositoryMock);
		}

		#region AddDiaryEntry
		[Fact]
		public async Task AddDiaryEntry_ValidInput_AddsDiaryEntryAndAssociatesTags()
		{
			// Arrange
			var entryAddDto = new DiaryEntryAddDTO
			{
				Title = "Test Title",
				Content = "Test Content",
				Mood = "Test Mood",
				CreatedAt = DateTime.UtcNow,
				Tags = new List<int> { 1, 2 }
			};
			var userId = "user123";

			var entryModel = entryAddDto.ToDiaryEntryFromAdd(userId);

			var tag1 = new Tag { TagId = 1, Name = "Tag1" };
			var tag2 = new Tag { TagId = 2, Name = "Tag2" };

			var entryTag1 = new EntryTag { TagId = 1, EntryId = 0 };
			var entryTag2 = new EntryTag { TagId = 2, EntryId = 0 };

			_entryRepositoryMock
					.AddDiaryEntryAsync(Arg.Any<DiaryEntry>())
					.Returns(entryModel);  // Mock adding the diary entry

			_tagRepositoryMock
					.GetTagByIdAsync(1)
					.Returns(tag1);  // Mock retrieving the first tag

			_tagRepositoryMock
					.GetTagByIdAsync(2)
					.Returns(tag2);  // Mock retrieving the second tag

			_entryTagRepositoryMock
					.AddEntryTagAsync(Arg.Any<EntryTag>())
					.Returns(entryTag1);  // Mock adding tags to the entry

			_entryTagRepositoryMock
					.AddEntryTagAsync(Arg.Any<EntryTag>())
					.Returns(entryTag2);  // Mock adding tags to the entry

			// Act
			var result = await _diaryEntryService.AddDiaryEntry(entryAddDto, userId);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(entryModel.ToDiaryEntryDto());
			await _entryRepositoryMock.Received(1).AddDiaryEntryAsync(Arg.Is<DiaryEntry>(x => x.UserId == userId));
			await _tagRepositoryMock.Received(1).GetTagByIdAsync(1);
			await _tagRepositoryMock.Received(1).GetTagByIdAsync(2);
			await _entryTagRepositoryMock.Received(2).AddEntryTagAsync(Arg.Any<EntryTag>());
		}

		[Fact]
		public async Task AddDiaryEntry_TagDoesNotExist_AddsOnlyOneTag()
		{
			// Arrange
			var entryAddDto = new DiaryEntryAddDTO
			{
				Title = "Test Title",
				Content = "Test Content",
				Mood = "Test Mood",
				CreatedAt = DateTime.UtcNow,
				Tags = new List<int> { 1, 99 }  // Tag with id 99 does not exist
			};
			var userId = "user123";

			var entryModel = entryAddDto.ToDiaryEntryFromAdd(userId);

			var tag1 = new Tag { TagId = 1, Name = "Tag1" };

			var entryTag1 = new EntryTag { TagId = 1, EntryId = 0 };

			_entryRepositoryMock
					.AddDiaryEntryAsync(Arg.Any<DiaryEntry>())
					.Returns(entryModel);  // Mock adding the diary entry

			_tagRepositoryMock
					.GetTagByIdAsync(1)
					.Returns(tag1);  // Mock retrieving the first tag

			_tagRepositoryMock
					.GetTagByIdAsync(99)
					.Returns((Tag?)null);  // Mock that tag 99 does not exist

			_entryTagRepositoryMock
					.AddEntryTagAsync(Arg.Any<EntryTag>())
					.Returns(entryTag1);  // Mock adding tags to the entry

			// Act
			var result = await _diaryEntryService.AddDiaryEntry(entryAddDto, userId);

			//Assert
			result.Should().NotBeNull();
			await _entryRepositoryMock.Received(1).AddDiaryEntryAsync(Arg.Is<DiaryEntry>(x => x.UserId == userId));
			await _tagRepositoryMock.Received(1).GetTagByIdAsync(1);
			await _tagRepositoryMock.Received(1).GetTagByIdAsync(99);  // Ensure the non-existent tag was checked
			await _entryTagRepositoryMock.Received(1).AddEntryTagAsync(Arg.Any<EntryTag>());
		}

		[Fact]
		public async Task AddDiaryEntry_NoTagsProvided_AddsDiaryEntryWithoutTags()
		{
			// Arrange
			var entryAddDto = new DiaryEntryAddDTO
			{
				Title = "Test Title",
				Content = "Test Content",
				Mood = "Test Mood",
				CreatedAt = DateTime.UtcNow,
				Tags = null // No tags provided
			};
			var userId = "user123";

			var entryModel = entryAddDto.ToDiaryEntryFromAdd(userId);

			_entryRepositoryMock
					.AddDiaryEntryAsync(Arg.Any<DiaryEntry>())
					.Returns(entryModel);  // Mock adding the diary entry

			// Act
			var result = await _diaryEntryService.AddDiaryEntry(entryAddDto, userId);

			// Assert
			result.Should().NotBeNull();
			await _entryRepositoryMock.Received(1).AddDiaryEntryAsync(Arg.Is<DiaryEntry>(x => x.UserId == userId));
			await _tagRepositoryMock.DidNotReceive().GetTagByIdAsync(Arg.Any<int>());  // Ensure no tags are retrieved
			await _entryTagRepositoryMock.DidNotReceive().AddEntryTagAsync(Arg.Any<EntryTag>());  // Ensure no tags were added
		}
		#endregion

		#region GetAllDiaryEntries

		#endregion

		#region GetDiaryEntryById

		#endregion

		#region GetDiaryEntryByDate

		#endregion

		#region GetDiaryEntryByUserId

		#endregion

		#region UpdateDiaryEntry

		#endregion

		#region DeleteDiaryEntry

		#endregion
	}
}

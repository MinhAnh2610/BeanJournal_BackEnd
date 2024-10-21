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
		[Fact]
		public async Task GetAllDiaryEntries_ValidEntries_ReturnsDiaryEntryDTOs()
		{
			// Arrange
			var diaryEntries = new List<DiaryEntry>
		{
				new DiaryEntry { EntryId = 1, Title = "Entry 1", Content = "Content 1", Mood = "Mood 1" },
				new DiaryEntry { EntryId = 2, Title = "Entry 2", Content = "Content 2", Mood = "Mood 2" }
		};

			_entryRepositoryMock
					.GetDiaryEntriesAsync()
					.Returns(diaryEntries);  // Mock retrieving diary entries

			// Act
			var result = await _diaryEntryService.GetAllDiaryEntries();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCount(2);
			await _entryRepositoryMock.Received(1).GetDiaryEntriesAsync();  // Verify that the repository was called once
		}

		[Fact]
		public async Task GetAllDiaryEntries_NoEntriesFound_ReturnsNull()
		{
			// Arrange
			_entryRepositoryMock
					.GetDiaryEntriesAsync()
					.Returns((ICollection<DiaryEntry>?)null);  // Mock returning null when no entries exist

			// Act
			var result = await _diaryEntryService.GetAllDiaryEntries();

			// Assert
			result.Should().BeNull();  // Verify that the result is null
			await _entryRepositoryMock.Received(1).GetDiaryEntriesAsync();  // Verify that the repository was called once
		}

		#endregion

		#region GetDiaryEntryById
		[Fact]
		public async Task GetDiaryEntryById_ValidId_ReturnsDiaryEntryDTO()
		{
			// Arrange
			var diaryEntry = new DiaryEntry { EntryId = 1, Title = "Sample Entry", Content = "Sample Content", Mood = "Sample Mood" };

			_entryRepositoryMock
					.GetDiaryEntryByIdAsync(1)
					.Returns(diaryEntry);  // Mock retrieving diary entry by ID

			// Act
			var result = await _diaryEntryService.GetDiaryEntryById(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result!.EntryId);  // Verify that the entry ID is correct
			Assert.Equal("Sample Entry", result.Title);  // Verify that the title is correct
			await _entryRepositoryMock.Received(1).GetDiaryEntryByIdAsync(1);  // Verify that the repository was called once with the correct ID
		}

		[Fact]
		public async Task GetDiaryEntryById_InvalidId_ReturnsNull()
		{
			// Arrange
			_entryRepositoryMock
					.GetDiaryEntryByIdAsync(2)
					.Returns((DiaryEntry?)null);  // Mock repository returning null for invalid ID

			// Act
			var result = await _diaryEntryService.GetDiaryEntryById(2);

			// Assert
			result.Should().BeNull();  // Verify that the result is null when no entry is found
			await _entryRepositoryMock.Received(1).GetDiaryEntryByIdAsync(2);  // Verify that the repository was called once with the correct ID
		}

		#endregion

		#region GetDiaryEntryByDate
		[Fact]
		public async Task GetDiaryEntryByDate_ValidDate_ReturnsDiaryEntryDTO()
		{
			// Arrange
			var date = new DateTime(2024, 10, 01);
			var diaryEntry = new DiaryEntry { EntryId = 1, Title = "Sample Entry", Content = "Sample Content", CreatedAt = date };

			_entryRepositoryMock
					.GetDiaryEntryByDateAsync(date)
					.Returns(diaryEntry);  // Mock retrieving diary entry by date

			// Act
			var result = await _diaryEntryService.GetDiaryEntryByDate(date);

			// Assert
			result.Should().NotBeNull();
			await _entryRepositoryMock.Received(1).GetDiaryEntryByDateAsync(date);  // Ensure repository was called once
		}

		[Fact]
		public async Task GetDiaryEntryByDate_InvalidDate_ReturnsNull()
		{
			// Arrange
			var date = new DateTime(2024, 10, 02);

			_entryRepositoryMock
					.GetDiaryEntryByDateAsync(date)
					.Returns((DiaryEntry?)null);  // Mock repository returning null for invalid date

			// Act
			var result = await _diaryEntryService.GetDiaryEntryByDate(date);

			// Assert
			result.Should().BeNull();  // Verify that the result is null when no entry is found
			await _entryRepositoryMock.Received(1).GetDiaryEntryByDateAsync(date);  // Ensure repository was called once
		}

		#endregion

		#region GetDiaryEntryByUserId
		[Fact]
		public async Task GetDiaryEntryByUserId_ValidUserId_ReturnsDiaryEntryDTOs()
		{
			// Arrange
			var userId = "user123";
			var diaryEntries = new List<DiaryEntry>
		{
				new DiaryEntry { EntryId = 1, Title = "First Entry", Content = "Content for first entry", UserId = userId },
				new DiaryEntry { EntryId = 2, Title = "Second Entry", Content = "Content for second entry", UserId = userId }
		};

			_entryRepositoryMock
					.GetDiaryEntriesByUserAsync(userId)
					.Returns(diaryEntries);  // Mock retrieving diary entries for a valid user

			// Act
			var result = await _diaryEntryService.GetDiaryEntryByUserId(userId);

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCount(2);
			await _entryRepositoryMock.Received(1).GetDiaryEntriesByUserAsync(userId);  // Ensure repository was called once
		}

		[Fact]
		public async Task GetDiaryEntryByUserId_InvalidUserId_ReturnsNull()
		{
			// Arrange
			var userId = "user123";

			_entryRepositoryMock
					.GetDiaryEntriesByUserAsync(userId)
					.Returns((ICollection<DiaryEntry>?)null);  // Mock repository returning null for invalid user

			// Act
			var result = await _diaryEntryService.GetDiaryEntryByUserId(userId);

			// Assert
			result.Should().BeNull();  // Verify that the result is null when no entries are found
			await _entryRepositoryMock.Received(1).GetDiaryEntriesByUserAsync(userId);  // Ensure repository was called once
		}
		#endregion

		#region UpdateDiaryEntry
		[Fact]
		public async Task UpdateDiaryEntry_ValidData_UpdatesEntryAndModifiesTags()
		{
			// Arrange
			var entryId = 1;
			var userId = "user123";
			var entryUpdateDTO = new DiaryEntryUpdateDTO
			{
				Title = "Updated Title",
				Tags = new List<int> { 1, 3 }
			};
			var existingTags = new List<EntryTag>
		{
				new EntryTag { EntryId = entryId, TagId = 1 },
				new EntryTag { EntryId = entryId, TagId = 2 }
		};
			var updatedEntry = new DiaryEntry { EntryId = entryId, Title = "Updated Title", UserId = userId };

			_entryRepositoryMock.UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>())
					.Returns(updatedEntry);  // Mock repository update

			_entryTagRepositoryMock.GetEntryTagByEntryIdAsync(entryId)
					.Returns(existingTags);  // Mock retrieving existing tags

			// Act
			var result = await _diaryEntryService.UpdateDiaryEntry(entryId, entryUpdateDTO, userId);

			// Assert
			result.Should().NotBeNull();  // Ensure result is not null
			result!.Title.Should().BeEquivalentTo(updatedEntry.Title);  // Validate updated entry title

			await _entryRepositoryMock.Received(1).UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>());  // Ensure repository was called
			await _entryTagRepositoryMock.Received(1).DeleteEntryTagByTagAndEntryAsync(entryId, 2);  // Verify tag was deleted
			await _entryTagRepositoryMock.Received(1).AddEntryTagAsync(Arg.Is<EntryTag>(et => et.TagId == 3));  // Verify new tag was added
		}

		[Fact]
		public async Task UpdateDiaryEntry_EntryNotFound_ReturnsNull()
		{
			// Arrange
			var entryId = 1;
			var userId = "user123";
			var entryUpdateDTO = new DiaryEntryUpdateDTO { Title = "Updated Title", Tags = new List<int> { 1, 3 } };

			_entryRepositoryMock.UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>())
					.Returns((DiaryEntry?)null);  // Mock repository returning null

			// Act
			var result = await _diaryEntryService.UpdateDiaryEntry(entryId, entryUpdateDTO, userId);

			// Assert
			result.Should().BeNull();  // Ensure result is null when entry is not found
			await _entryRepositoryMock.Received(1).UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>());  // Ensure repository was called
			await _entryTagRepositoryMock.DidNotReceive().GetEntryTagByEntryIdAsync(entryId);  // No tag operations should occur
		}

		[Fact]
		public async Task UpdateDiaryEntry_NoExistingTags_AddsNewTagsOnly()
		{
			// Arrange
			var entryId = 1;
			var userId = "user123";
			var entryUpdateDTO = new DiaryEntryUpdateDTO { Title = "Updated Title", Tags = new List<int> { 1, 2 } };
			var updatedEntry = new DiaryEntry { EntryId = entryId, Title = "Updated Title", UserId = userId };

			_entryRepositoryMock.UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>())
					.Returns(updatedEntry);  // Mock repository update

			_entryTagRepositoryMock.GetEntryTagByEntryIdAsync(entryId)
					.Returns((ICollection<EntryTag>?)null);  // No existing tags

			// Act
			var result = await _diaryEntryService.UpdateDiaryEntry(entryId, entryUpdateDTO, userId);

			// Assert
			result.Should().NotBeNull();
			await _entryTagRepositoryMock.Received(1).AddEntryTagAsync(Arg.Is<EntryTag>(et => et.TagId == 1));  // New tag added
			await _entryTagRepositoryMock.Received(1).AddEntryTagAsync(Arg.Is<EntryTag>(et => et.TagId == 2));  // New tag added
			await _entryTagRepositoryMock.DidNotReceive().DeleteEntryTagByTagAndEntryAsync(Arg.Any<int>(), Arg.Any<int>());  // No tag removal
		}

		[Fact]
		public async Task UpdateDiaryEntry_NoTagsToAddOrRemove_NoTagOperations()
		{
			// Arrange
			var entryId = 1;
			var userId = "user123";
			var entryUpdateDTO = new DiaryEntryUpdateDTO { Title = "Updated Title", Tags = new List<int> { 1 } };
			var existingTags = new List<EntryTag>
		{
				new EntryTag { EntryId = entryId, TagId = 1 }
		};
			var updatedEntry = new DiaryEntry { EntryId = entryId, Title = "Updated Title", UserId = userId };

			_entryRepositoryMock.UpdateDiaryEntryAsync(entryId, Arg.Any<DiaryEntry>())
					.Returns(updatedEntry);  // Mock repository update

			_entryTagRepositoryMock.GetEntryTagByEntryIdAsync(entryId)
					.Returns(existingTags);  // Existing tags match the new tags

			// Act
			var result = await _diaryEntryService.UpdateDiaryEntry(entryId, entryUpdateDTO, userId);

			// Assert
			result.Should().NotBeNull();
			await _entryTagRepositoryMock.DidNotReceive().DeleteEntryTagByTagAndEntryAsync(Arg.Any<int>(), Arg.Any<int>());  // No deletion
			await _entryTagRepositoryMock.DidNotReceive().AddEntryTagAsync(Arg.Any<EntryTag>());  // No addition
		}
		#endregion

		#region DeleteDiaryEntry
		[Fact]
		public async Task DeleteDiaryEntry_ValidEntryId_DeletesEntryAndTags()
		{
			// Arrange
			var entryId = 1;
			var diaryEntry = new DiaryEntry { EntryId = entryId, Title = "Test Entry" };

			_entryRepositoryMock.DeleteDiaryEntryAsync(entryId)
					.Returns(diaryEntry);  // Mock repository returning the deleted diary entry

			// Act
			var result = await _diaryEntryService.DeleteDiaryEntry(entryId);

			// Assert
			result.Should().NotBeNull();  // Ensure result is not null

			await _entryRepositoryMock.Received(1).DeleteDiaryEntryAsync(entryId);  // Ensure repository delete was called
			await _entryTagRepositoryMock.Received(1).DeleteEntryTagsAsync(entryId);  // Ensure tags deletion was called
		}

		[Fact]
		public async Task DeleteDiaryEntry_EntryNotFound_ReturnsNull()
		{
			// Arrange
			var entryId = 1;

			_entryRepositoryMock.DeleteDiaryEntryAsync(entryId)
					.Returns((DiaryEntry?)null);  // Mock repository returning null when entry doesn't exist

			// Act
			var result = await _diaryEntryService.DeleteDiaryEntry(entryId);

			// Assert
			result.Should().BeNull(); // Ensure result is null when the entry is not found
			await _entryRepositoryMock.Received(1).DeleteDiaryEntryAsync(entryId);  // Ensure repository delete was called
			await _entryTagRepositoryMock.DidNotReceive().DeleteEntryTagsAsync(entryId);  // No tag deletion should occur
		}
		#endregion
	}
}

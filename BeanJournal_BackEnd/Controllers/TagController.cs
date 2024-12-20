﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Tag;

namespace BeanJournal_BackEnd.Controllers
{
	/// <summary>
	/// Tag APIs
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TagController : ControllerBase
	{
		private readonly ITagService _tagService;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tagService"></param>
		public TagController(ITagService tagService)
		{
			_tagService = tagService;
		}

		/// <summary>
		/// Get all tags
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> GetAll()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tags = await _tagService.GetAllTags();
			if (tags == null)
			{
				return NoContent();
			}
			return Ok(tags);
		}

		/// <summary>
		/// Get a specific tag by its Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id:int}")]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tag = await _tagService.GetTagById(id);
			if (tag == null)
			{
				return NoContent();
			}
			return Ok(tag);
		}

		/// <summary>
		/// Create a tag
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromForm] TagAddDTO tagRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tagResponse = await _tagService.AddTag(tagRequest);
			return CreatedAtAction(nameof(GetById), new { id = tagResponse.TagId }, tagResponse);
		}

		/// <summary>
		/// Update a tag based on its Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="tagRequest"></param>
		/// <returns></returns>
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromForm] TagAddDTO tagRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tagResponse = await _tagService.UpdateTag(id, tagRequest);
			if (tagResponse == null)
			{
				return BadRequest("Tag does not exist");
			}
			return Ok(tagResponse);
		}

		/// <summary>
		/// Delete a tag based on its Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tagResponse = await _tagService.DeleteTag(id);
			if (tagResponse == null)
			{
				return BadRequest("Tag does not exist");
			}
			return Ok(tagResponse);
		}
	}
}

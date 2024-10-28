using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;
using ServiceContracts.DTO.MediaAttachment;
using Services;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeanJournal_BackEnd.Controllers
{
  /// <summary>
  /// Diary Entry APIs
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class DiaryEntryController : ControllerBase
  {
    private readonly IDiaryEntryService  _entryService;
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entryService"></param>
    /// <param name="userManager"></param>
    public DiaryEntryController(IDiaryEntryService entryService,
                                UserManager<ApplicationUser> userManager)
    {
      _entryService = entryService;
      _userManager = userManager;
    }

    /// <summary>
    /// Get all diary entries from all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entries = await _entryService.GetAllDiaryEntries();
      if (entries == null)
      {
        return NoContent();
      }
      return Ok(entries);
    }

    /// <summary>
    /// Get all diary entries from the authenticated user
    /// </summary>
    /// <returns></returns>
    [HttpGet("user-diary")]
    public async Task<IActionResult> GetByUser()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
      var user = await _userManager.GetUserAsync(User);
      var entries = await _entryService.GetDiaryEntryByUserId(user!.Id);
      if (entries == null)
      {
        return NoContent();
      }
      return Ok(entries);
    }

    /// <summary>
    /// Get a specific diary entry based on its Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entry = await _entryService.GetDiaryEntryById(id);
      if (entry == null)
      {
        return NotFound();
      }
      return Ok(entry);
    }

    /// <summary>
    /// Get a specific diary entry of an authenticated user based on its date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{date:datetime}")]
		[Authorize]
    public async Task<IActionResult> GetByDate([FromRoute] DateTime date)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entry = await _entryService.GetDiaryEntryByDate(date);
      if (entry == null)
      {
        return NotFound();
      }
      return Ok(entry);
    }

    /// <summary>
    /// Create a new diary entry
    /// </summary>
    /// <param name="entryAddDto"></param>
    /// <returns></returns>
    [HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] DiaryEntryAddDTO entryAddDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var user = await _userManager.GetUserAsync(User);
      var entryResponse = await _entryService.AddDiaryEntry(entryAddDto, user!.Id);

      return CreatedAtAction(nameof(GetById), new { id = entryResponse.EntryId }, entryResponse);
    }

    /// <summary>
    /// Update a diary entry
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entryUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DiaryEntryUpdateDTO entryUpdateDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var user = await _userManager.GetUserAsync(User);
      var entryResponse = await _entryService.UpdateDiaryEntry(id, entryUpdateDto, user!.Id);

      return Ok(entryResponse);
    }

    /// <summary>
    /// Delete a diary entry
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entryResponse = await _entryService.DeleteDiaryEntry(id);
      return Ok(entryResponse);
    }
  }
}

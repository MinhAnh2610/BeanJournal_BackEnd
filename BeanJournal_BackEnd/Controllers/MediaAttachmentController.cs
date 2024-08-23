using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace BeanJournal_BackEnd.Controllers
{
  /// <summary>
  /// Media Attachment APIs
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class MediaAttachmentController : ControllerBase
  {
    private readonly IMediaAttachmentService _mediaAttachmentService;
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediaAttachmentService"></param>
    /// <param name="userManager"></param>
    public MediaAttachmentController(IMediaAttachmentService mediaAttachmentService,
                                     UserManager<ApplicationUser> userManager)
    {
      _mediaAttachmentService = mediaAttachmentService;
      _userManager = userManager;
    }

    /// <summary>
    /// Get all media attachments from all users
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
      var mediaAttachmentResponses = await _mediaAttachmentService.GetAllMediaAttachments();
      if (mediaAttachmentResponses == null)
      {
        return NoContent();
      }
      return Ok(mediaAttachmentResponses);
    }

    /// <summary>
    /// Get a specific media attachment by its Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var mediaAttachmentResponse = await _mediaAttachmentService.GetMediaAttachmentById(id);
      if (mediaAttachmentResponse == null)
      {
        return NoContent();
      }
      return Ok(mediaAttachmentResponse);
    }

    /// <summary>
    /// Get all media attachments from the authenticated user
    /// </summary>
    /// <returns></returns>
    [HttpGet("user-media")]
    public async Task<IActionResult> GetByUser()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var user = await _userManager.GetUserAsync(User);
      var mediaAttachmentResponses = await _mediaAttachmentService.GetAllMediaAttachmentsByUser(user!.Id);
      if (mediaAttachmentResponses == null)
      {
        return NoContent();
      }
      return Ok(mediaAttachmentResponses);
    }

    /// <summary>
    /// Delete a media attachment by its Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var mediaAttachmentResponse = await _mediaAttachmentService.DeleteMediaAttachment(id);
      if (mediaAttachmentResponse == null)
      {
        return BadRequest("Media Attachment does not exist");
      }
      return Ok(mediaAttachmentResponse);
    }
  }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.DiaryEntry;

namespace BeanJournal_BackEnd.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DiaryEntryController : ControllerBase
  {
    private readonly IDiaryEntryService _entryService;
    public DiaryEntryController(IDiaryEntryService entryService)
    {
      _entryService = entryService;
    }

    [HttpGet]
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

    [HttpGet]
    [Route("{id:int}")]
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

    [HttpGet]
    [Route("{date:datetime}")]
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DiaryEntryAddDTO entryAddDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entryResponse = await _entryService.AddDiaryEntry(entryAddDto);
      return CreatedAtAction(nameof(GetById), new { entryId = entryResponse.EntryId }, entryResponse);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DiaryEntryUpdateDTO entryUpdateDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var entryResponse = await _entryService.UpdateDiaryEntry(entryUpdateDto);
      return Ok(entryResponse);
    }

    [HttpDelete]
    [Route("{id:int}")]
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

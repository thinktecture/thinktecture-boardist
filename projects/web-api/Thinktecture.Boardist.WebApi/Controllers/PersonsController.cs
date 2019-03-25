using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class PersonsController : ControllerBase
  {
    private readonly PersonsService _personsService;
    private readonly SyncService _syncService;

    public PersonsController(PersonsService personsService, SyncService syncService)
    {
      _personsService = personsService;
      _syncService = syncService;
    }

    [HttpGet]
    public async Task<ActionResult<PersonDto[]>> ListAsync()
    {
      return Ok(await _personsService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> SingleAsync(Guid id)
    {
      var result = await _personsService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreateAsync([FromBody] PersonDto person)
    {
      return Ok(await _personsService.CreateAsync(person));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      await _personsService.DeleteAsync(id);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<PersonDto>> UpdateAsync([FromBody] PersonDto person)
    {
      await _personsService.UpdateAsync(person);
      return Ok();
    }
    
    [HttpGet("sync")]
    public async Task<ActionResult<SyncDto<PersonDto>>> SyncAsync([FromQuery] string timestamp = null)
    {
      return Ok(await _syncService.SyncAsync<Person, PublisherDto>(timestamp));
    }
  }
}

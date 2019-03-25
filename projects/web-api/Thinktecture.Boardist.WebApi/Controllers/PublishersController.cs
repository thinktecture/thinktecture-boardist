using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class PublishersController : ControllerBase
  {
    private readonly PublishersService _publishersService;
    private readonly SyncService _syncService;

    public PublishersController(PublishersService publishersService, SyncService syncService)
    {
      _publishersService = publishersService;
      _syncService = syncService;
    }

    [HttpGet]
    public async Task<ActionResult<PublisherDto[]>> ListAsync()
    {
      return Ok(await _publishersService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> SingleAsync(Guid id)
    {
      var result = await _publishersService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> CreateAsync([FromBody] PublisherDto publisher)
    {
      return Ok(await _publishersService.CreateAsync(publisher));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      await _publishersService.DeleteAsync(id);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<PublisherDto>> UpdateAsync([FromBody] PublisherDto publisher)
    {
      await _publishersService.UpdateAsync(publisher);
      return Ok();
    }

    [HttpGet("sync")]
    public async Task<ActionResult<SyncDto<PublisherDto>>> SyncAsync([FromQuery] string timestamp = null)
    {
      return Ok(await _syncService.SyncAsync<Publisher, PublisherDto>(timestamp));
    }
  }
}

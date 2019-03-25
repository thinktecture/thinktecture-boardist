using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class MechanicsController : ControllerBase
  {
    private readonly MechanicsService _mechanicsService;
    private readonly SyncService _syncService;

    public MechanicsController(MechanicsService mechanicsService, SyncService syncService)
    {
      _mechanicsService = mechanicsService;
      _syncService = syncService;
    }

    [HttpGet]
    public async Task<ActionResult<MechanicDto[]>> ListAsync()
    {
      return Ok(await _mechanicsService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MechanicDto>> SingleAsync(Guid id)
    {
      var result = await _mechanicsService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MechanicDto>> CreateAsync([FromBody] MechanicDto mechanic)
    {
      return Ok(await _mechanicsService.CreateAsync(mechanic));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      await _mechanicsService.DeleteAsync(id);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<MechanicDto>> UpdateAsync([FromBody] MechanicDto mechanic)
    {
      await _mechanicsService.UpdateAsync(mechanic);
      return Ok();
    }
    
    [HttpGet("sync")]
    public async Task<ActionResult<SyncDto<MechanicDto>>> SyncAsync([FromQuery] string timestamp = null)
    {
      return Ok(await _syncService.SyncAsync<Mechanic, PublisherDto>(timestamp));
    }
  }
}

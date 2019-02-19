using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class MechanicsController : ControllerBase
  {
    private readonly MechanicsService _mechanicsService;

    public MechanicsController(MechanicsService mechanicsService)
    {
      _mechanicsService = mechanicsService;
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
      if (await _mechanicsService.DeleteAsync(id))
      {
        return Ok();
      }

      return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<MechanicDto>> UpdateAsync([FromBody] MechanicDto mechanic)
    {
      await _mechanicsService.UpdateAsync(mechanic);
      return Ok();
    }
  }
}

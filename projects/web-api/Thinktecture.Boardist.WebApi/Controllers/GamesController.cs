using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class GamesController : ControllerBase
  {
    private readonly GamesService _gamesService;

    public GamesController(GamesService gamesService)
    {
      _gamesService = gamesService;
    }

    [HttpGet]
    public async Task<ActionResult<GameDto[]>> ListAsync()
    {
      return Ok(await _gamesService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> SingleAsync(Guid id)
    {
      var result = await _gamesService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }
  }
}

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
    private readonly BoardGameGeekImporter _importer;

    public GamesController(GamesService gamesService, BoardGameGeekImporter importer)
    {
      _gamesService = gamesService;
      _importer = importer;
    }

    [HttpPost("{id}/import")]
    public async Task<ActionResult<bool>> Import(Guid id)
    {
      return Ok(await _importer.Import(id));
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<bool>> Lookup(string query)
    {
      return Ok(await _importer.Lookup(query));
    }

    [HttpGet]
    public async Task<ActionResult<GameDto[]>> ListAsync(bool expansion = true)
    {
      return Ok(await _gamesService.GetAllAsync(expansion));
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

    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateAsync([FromBody] GameDto game)
    {
      return Ok(await _gamesService.CreateAsync(game));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      if (await _gamesService.DeleteAsync(id))
      {
        return Ok();
      }

      return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<GameDto>> UpdateAsync([FromBody] GameDto game)
    {
      await _gamesService.UpdateAsync(game);
      return Ok();
    }
  }
}

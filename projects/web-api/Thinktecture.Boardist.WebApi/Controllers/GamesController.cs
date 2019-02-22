using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class GamesController : ControllerBase
  {
    private readonly GamesService _gamesService;
    private readonly BoardGameGeekImporter _importer;
    private readonly SyncService _syncService;

    public GamesController(GamesService gamesService, BoardGameGeekImporter importer, SyncService syncService)
    {
      _gamesService = gamesService;
      _importer = importer;
      _syncService = syncService;
    }

    [HttpPost("{id}/import")]
    public async Task<ActionResult<bool>> Import(Guid id, bool overwrite = false)
    {
      return Ok(await _importer.Import(id, overwrite));
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
      await _gamesService.DeleteAsync(id);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<GameDto>> UpdateAsync([FromBody] GameDto game)
    {
      await _gamesService.UpdateAsync(game);
      return Ok();
    }
    
    [HttpGet("sync/{timestamp?}")]
    public async Task<ActionResult<SyncDto<GameDto>>> SyncAsync(string timestamp)
    {
      return Ok(await _syncService.SyncAsync<Game, GameDto>(timestamp));
    }

    [HttpHead("{id}")]
    public IActionResult HasRules(Guid id)
    {
      if (_gamesService.HasRules(id))
      {
        return Ok(true);
      }

      return NoContent();
    }
  }
}

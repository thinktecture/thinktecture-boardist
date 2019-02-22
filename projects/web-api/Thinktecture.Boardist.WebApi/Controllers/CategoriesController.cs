using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly CategoriesService _categoriesService;
    private readonly SyncService _syncService;

    public CategoriesController(CategoriesService categoriesService, SyncService syncService)
    {
      _categoriesService = categoriesService;
      _syncService = syncService;
    }

    [HttpGet]
    public async Task<ActionResult<CategoryDto[]>> ListAsync()
    {
      return Ok(await _categoriesService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> SingleAsync(Guid id)
    {
      var result = await _categoriesService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateAsync([FromBody] CategoryDto category)
    {
      return Ok(await _categoriesService.CreateAsync(category));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      await _categoriesService.DeleteAsync(id);

      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDto>> UpdateAsync([FromBody] CategoryDto category)
    {
      await _categoriesService.UpdateAsync(category);
      return Ok();
    }
    
    [HttpGet("sync/{timestamp?}")]
    public async Task<ActionResult<SyncDto<CategoryDto>>> SyncAsync(string timestamp)
    {
      return Ok(await _syncService.SyncAsync<Category, CategoryDto>(timestamp));
    }
  }
}

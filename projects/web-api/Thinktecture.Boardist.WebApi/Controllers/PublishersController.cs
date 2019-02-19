using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class PublishersController : ControllerBase
  {
    private readonly PublishersService _publishersService;

    public PublishersController(PublishersService publishersService)
    {
      _publishersService = publishersService;
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
      if (await _publishersService.DeleteAsync(id))
      {
        return Ok();
      }

      return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<PublisherDto>> UpdateAsync([FromBody] PublisherDto publisher)
    {
      await _publishersService.UpdateAsync(publisher);
      return Ok();
    }
  }
}

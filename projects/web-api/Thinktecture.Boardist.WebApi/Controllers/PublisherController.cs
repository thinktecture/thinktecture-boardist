using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class PublisherController : ControllerBase
  {
    private readonly PublisherService _publisherService;

    public PublisherController(PublisherService publisherService)
    {
      _publisherService = publisherService;
    }

    [HttpGet]
    public async Task<ActionResult<PublisherDto[]>> List()
    {
      return Ok(await _publisherService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> Single(Guid id)
    {
      var result = await _publisherService.Get(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> Create([FromBody] PublisherDto publisher)
    {
      return Ok(await _publisherService.Create(publisher));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      if (await _publisherService.Delete(id))
      {
        return Ok();
      }

      return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<PublisherDto>> Update([FromBody] PublisherDto publisher)
    {
      await _publisherService.Update(publisher);
      return Ok();
    }
  }
}

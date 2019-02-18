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
  }
}

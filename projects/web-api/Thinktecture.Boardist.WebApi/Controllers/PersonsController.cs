using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class PersonsController : ControllerBase
  {
    private readonly PersonsService _personsService;

    public PersonsController(PersonsService personsService)
    {
      _personsService = personsService;
    }

    [HttpGet]
    public async Task<ActionResult<PersonDto[]>> ListAsync()
    {
      return Ok(await _personsService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> SingleAsync(Guid id)
    {
      var result = await _personsService.GetAsync(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreateAsync([FromBody] PersonDto person)
    {
      return Ok(await _personsService.CreateAsync(person));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      if (await _personsService.DeleteAsync(id))
      {
        return Ok();
      }

      return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<PersonDto>> UpdateAsync([FromBody] PersonDto person)
    {
      await _personsService.UpdateAsync(person);
      return Ok();
    }
  }
}
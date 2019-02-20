using System;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.Models;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class BinariesController : ControllerBase
  {
    private readonly FilesService _filesService;

    public BinariesController(FilesService filesService)
    {
      _filesService = filesService;
    }
    
    [HttpGet("{id}/logo")]
    public IActionResult Logo(Guid id)
    {
      var result = _filesService.Load(id, FileCategory.Logo);

      return File(result.Stream, result.MimeType);
    }
  }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.Boardist.WebApi.DTOs;
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

    [HttpGet("{id}/rules")]
    public IActionResult Rules(Guid id)
    {
      var result = _filesService.Load(id, FileCategory.Rules);

      return File(result.Stream, result.MimeType);
    }

    [HttpPost("logo")]
    public async Task<IActionResult> UploadLogo([FromForm] BinaryUploadDto binaryUploadDto)
    {
      return await Upload(binaryUploadDto, FileCategory.Logo);
    }

    [HttpPost("rules")]
    public async Task<IActionResult> UploadRules([FromForm] BinaryUploadDto binaryUploadDto)
    {
      return await Upload(binaryUploadDto, FileCategory.Rules);
    }

    private async Task<IActionResult> Upload(BinaryUploadDto binaryUploadDto, FileCategory category)
    {
      if (binaryUploadDto?.Id == Guid.Empty || binaryUploadDto?.File == null)
      {
        return BadRequest();
      }

      using (var stream = binaryUploadDto.File.OpenReadStream())
      {
        await _filesService.Save(stream, binaryUploadDto.Id, category, Path.GetExtension(binaryUploadDto.File.FileName));
      }

      return Ok();
    }
  }
}

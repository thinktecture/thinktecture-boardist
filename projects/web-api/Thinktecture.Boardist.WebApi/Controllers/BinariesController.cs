using System;
using System.IO;
using System.Threading.Tasks;
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
    private readonly BoardGameGeekImporter _boardGameGeekImporter;

    public BinariesController(FilesService filesService, BoardGameGeekImporter boardGameGeekImporter)
    {
      _filesService = filesService;
      _boardGameGeekImporter = boardGameGeekImporter;
    }

    [HttpGet("{id}/logo")]
    public async Task<IActionResult> LogoAsync(Guid id)
    {
      if (!_filesService.Exists(id, FileCategory.Logo))
      {
        await _boardGameGeekImporter.ImportImageAsync(id);
      }

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
    public async Task<IActionResult> UploadLogoAsync([FromForm] BinaryUploadDto binaryUploadDto)
    {
      return await UploadAsync(binaryUploadDto, FileCategory.Logo);
    }

    [HttpPost("rules")]
    public async Task<IActionResult> UploadRulesAsync([FromForm] BinaryUploadDto binaryUploadDto)
    {
      return await UploadAsync(binaryUploadDto, FileCategory.Rules);
    }

    private async Task<IActionResult> UploadAsync(BinaryUploadDto binaryUploadDto, FileCategory category)
    {
      if (binaryUploadDto?.Id == Guid.Empty || binaryUploadDto?.File == null)
      {
        return BadRequest();
      }

      using (var stream = binaryUploadDto.File.OpenReadStream())
      {
        await _filesService.SaveAsync(stream, binaryUploadDto.Id, category, Path.GetExtension(binaryUploadDto.File.FileName));
      }

      return Ok();
    }
  }
}

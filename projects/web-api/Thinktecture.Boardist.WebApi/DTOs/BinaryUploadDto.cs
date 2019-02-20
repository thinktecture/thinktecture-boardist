using System;
using Microsoft.AspNetCore.Http;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public class BinaryUploadDto
  {
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
  }
}

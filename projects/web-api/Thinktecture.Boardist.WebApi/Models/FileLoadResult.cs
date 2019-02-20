using System.IO;

namespace Thinktecture.Boardist.WebApi.Models
{
  public class FileLoadResult
  {
    public string MimeType { get; set; }
    public Stream Stream { get; set; }
  }
}

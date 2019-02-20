using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MimeTypes;
using Thinktecture.Boardist.WebApi.Models;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class FilesService
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    private const string BaseFolderName = "Assets";
    private const string NoImageAvailableFileName = "no-image-available.png";

    private string BaseDirectory => Path.Combine(_hostingEnvironment.ContentRootPath, BaseFolderName);

    public FilesService(IHostingEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;
    }

    public async Task Save(Stream stream, Guid fileId, FileCategory category, MediaTypeHeaderValue contentType)
    {
      var fileIdName = fileId.ToString();
      var fileName = $"{FileCategoryToFileName(category)}{MimeTypeMap.GetExtension(contentType.MediaType)}";
      
      EnsureFolderExists(Path.Combine(BaseDirectory, fileIdName));

      using (var fileStream = new FileStream(Path.Combine(BaseDirectory, fileIdName, fileName), FileMode.Create))
      {
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream);
      }
    }

    public FileLoadResult Load(Guid fileId, FileCategory category)
    {
      var fileIdName = fileId.ToString();
      var fileToLoad = GetSingleFile(category, fileIdName);

      if (string.IsNullOrWhiteSpace(fileToLoad))
      {
        fileToLoad = Path.Combine(BaseDirectory, NoImageAvailableFileName);
      }

      var fileExtension = Path.GetExtension(fileToLoad).Substring(1);
      var mimeType = MimeTypeMap.GetMimeType(fileExtension);

      var fileStream = new FileStream(fileToLoad, FileMode.Open);

      return new FileLoadResult()
      {
        Stream = fileStream,
        MimeType = mimeType
      };
    }

    public void Delete(Guid fileId)
    {
      var directoryPath = Path.Combine(BaseDirectory, fileId.ToString());
      
      if (Directory.Exists(directoryPath)) {
        Directory.Delete(directoryPath, true);
      }
    }

    private string GetSingleFile(FileCategory fileCategory, string fileIdName)
    {
      var path = Path.Combine(BaseDirectory, fileIdName);

      if (!Directory.Exists(path))
      {
        return string.Empty;
      }
      
      var fileToDelete = Directory.GetFiles(path)
        .SingleOrDefault(file => file.Contains(Path.Combine(path, FileCategoryToFileName(fileCategory))));
      return fileToDelete;
    }

    private void EnsureFolderExists(string folder)
    {
      Directory.CreateDirectory(folder);
    }

    private string FileCategoryToFileName(FileCategory fileCategory)
    {
      switch (fileCategory)
      {
        case FileCategory.Logo: return "logo";
        default: return "rules";
      }
    }
  }
}

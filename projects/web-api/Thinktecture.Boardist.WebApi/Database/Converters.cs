using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thinktecture.Boardist.WebApi.Extensions;

namespace Thinktecture.Boardist.WebApi.Database
{
  public static class Converters
  {
    public static ValueConverter<ulong, byte[]> RowVersionConverter()
    {
      return new ValueConverter<ulong, byte[]>(
        l => l.ToBigEndianBytes(),
        b => b.ToBigEndianUInt64()
      );
    }
  }
}

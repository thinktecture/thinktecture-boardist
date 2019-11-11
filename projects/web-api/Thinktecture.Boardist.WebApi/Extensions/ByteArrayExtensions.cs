using System;
using System.Linq;

namespace Thinktecture.Boardist.WebApi.Extensions
{
  public static class ByteArrayExtensions
  {
    public static ulong ToBigEndianUInt64(this byte[] value) => BitConverter.ToUInt64(value.Reverse().ToArray());
  }
}

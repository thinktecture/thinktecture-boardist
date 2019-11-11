using System;
using System.Linq;

namespace Thinktecture.Boardist.WebApi.Extensions
{
  public static class UInt64Extensions
  {
    public static byte[] ToBigEndianBytes(this ulong value) => BitConverter.GetBytes(value).Reverse().ToArray();
  }
}

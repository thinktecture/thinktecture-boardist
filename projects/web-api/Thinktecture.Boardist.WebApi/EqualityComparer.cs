using System;
using System.Collections.Generic;

namespace Thinktecture.Boardist.WebApi
{
  public class EqualityComparer<T> : IEqualityComparer<T>
  {
    private readonly Func<T, T, bool> _equalsFunction;

    public EqualityComparer(Func<T, T, bool> equalsFunction)
    {
      _equalsFunction = equalsFunction;
    }

    public bool Equals(T a, T b)
    {
      return _equalsFunction(a, b);
    }

    public int GetHashCode(T obj)
    {
      return obj.GetHashCode();
    }
  }
}

using System.Linq;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.Extensions
{
  public static class IQueryableExtensions
  {
    public static IQueryable<T> WithoutDeleted<T>(this IQueryable<T> query)
      where T : ISyncable
    {
      return query.Where(p => !p.IsDeleted);
    }
  }
}

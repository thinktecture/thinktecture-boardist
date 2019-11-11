using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Extensions;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class SyncService
  {
    private static readonly string EmptyRowVersion = Convert.ToBase64String(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
    private readonly BoardistContext _boardistContext;
    private readonly IMapper _mapper;

    public SyncService(BoardistContext boardistContext, IMapper mapper)
    {
      _boardistContext = boardistContext;
      _mapper = mapper;
    }

    public async Task<SyncDto<TResult>> SyncAsync<TSource, TResult>(string value)
      where TSource : Syncable
      where TResult : SyncableDto
    {
      var rowVersion = Convert.FromBase64String(value ?? EmptyRowVersion).ToBigEndianUInt64();

      var timestamp = await _boardistContext.GetMinActiveRowVersionAsync();

      var baseQuery = _boardistContext.Set<TSource>().Where(p => p.RowVersion >= rowVersion);

      var changed = await _mapper.ProjectTo<TResult>(baseQuery.WithoutDeleted()).ToListAsync();
      var deleted = await baseQuery.Where(p => p.IsDeleted).Select(p => p.Id).ToListAsync();

      return new SyncDto<TResult>
      {
        Timestamp = timestamp.ToBigEndianBytes(),
        Changed = changed,
        Deleted = deleted
      };
    }
  }
}

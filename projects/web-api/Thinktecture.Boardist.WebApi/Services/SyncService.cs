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
    private readonly BoardistContext _boardistContext;
    private readonly IMapper _mapper;

    public SyncService(BoardistContext boardistContext, IMapper mapper)
    {
      _boardistContext = boardistContext;
      _mapper = mapper;
    }

    public async Task<SyncDto<TResult>> SyncAsync<TSource, TResult>(string timestamp)
      where TSource : Syncable
      where TResult : SyncableDto
    {
      var rowVersion = Convert.FromBase64String(timestamp ?? string.Empty);

      var baseQuery = _boardistContext.Set<TSource>().Where(p => (ulong)(object)p.RowVersion >= (ulong)(object)rowVersion);

      var changed = await _mapper.ProjectTo<TResult>(baseQuery.WithoutDeleted()).ToListAsync();
      var deleted = await baseQuery.Where(p => p.IsDeleted).Select(p => p.Id).ToListAsync();

      return new SyncDto<TResult>()
      {
        Timestamp = await _boardistContext.GetMinActiveRowVersionAsync(),
        Changed = changed,
        Deleted = deleted
      };
    }
  }
}

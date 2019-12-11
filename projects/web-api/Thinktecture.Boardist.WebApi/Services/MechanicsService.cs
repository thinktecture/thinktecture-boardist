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
  public class MechanicsService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;

    public MechanicsService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<MechanicDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<MechanicDto>(_boardistContext.Mechanics.WithoutDeleted().OrderBy(p => p.Name)).ToArrayAsync();
    }

    public async Task<MechanicDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<MechanicDto>(_boardistContext.Mechanics.WithoutDeleted().Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<MechanicDto> CreateAsync(MechanicDto mechanic)
    {
      var dbMechanic = _mapper.Map<Mechanic>(mechanic);

      await _boardistContext.Mechanics.AddAsync(dbMechanic);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Mechanic, MechanicDto>(dbMechanic);
    }

    public async Task DeleteAsync(Guid id)
    {
      var dbMechanic = await _boardistContext.Mechanics.SingleAsync(p => p.Id == id);

      dbMechanic.IsDeleted = true;

      await _boardistContext.SaveChangesAsync();
    }

    public async Task<MechanicDto> UpdateAsync(MechanicDto mechanic)
    {
      var dbMechanic = new Mechanic() { Id = mechanic.Id };

      _boardistContext.Attach(dbMechanic);

      _mapper.Map(mechanic, dbMechanic);

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Mechanic, MechanicDto>(dbMechanic);
    }
  }
}

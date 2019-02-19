using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class CategoriesService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;

    public CategoriesService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<CategoryDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<CategoryDto>(_boardistContext.Categories).ToArrayAsync();
    }

    public async Task<CategoryDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<CategoryDto>(_boardistContext.Categories.Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto category)
    {
      var dbCategory = new Category() {Name = category.Name, Id = Guid.NewGuid(), BoardGameGeekId = category.BoardGameGeekId};

      await _boardistContext.Categories.AddAsync(dbCategory);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Category, CategoryDto>(dbCategory);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var dbCategory = new Category() {Id = id};

      _boardistContext.Entry(dbCategory).State = EntityState.Deleted;

      try
      {
        await _boardistContext.SaveChangesAsync();

        return true;
      }
      catch (DbUpdateConcurrencyException)
      {
        return false;
      }
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto category)
    {
      var dbCategory = new Category() {Id = category.Id};

      _boardistContext.Attach(dbCategory);

      dbCategory.Name = category.Name;
      dbCategory.BoardGameGeekId = category.BoardGameGeekId;

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Category, CategoryDto>(dbCategory);
    }
  }
}

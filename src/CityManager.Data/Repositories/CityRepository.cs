using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityManager.Domain.Entities;
using CityManager.Domain.Queries;
using CityManager.Domain.Repositories.Interfaces;
using CityManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CityManager.Data.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(CityManagerContext dbContext) : base(dbContext)
        {

        }

        public async Task<PaginatedQueryResult<City>> GetAllAsync(CityQuery cityQuery)
        {
            var query = _dbContext.Set<City>().AsQueryable();

            if (!string.IsNullOrEmpty(cityQuery.Name))
            {
                query = query.Where(x => EF.Functions.ILike(x.Name, $"%{cityQuery.Name}%"));
            }
            if (!string.IsNullOrEmpty(cityQuery.UF))
            {
                query = query.Where(x => EF.Functions.ILike(x.UF, $"%{cityQuery.UF}%"));
            }
            if (!string.IsNullOrEmpty(cityQuery.Region))
            {
                query = query.Where(x => EF.Functions.ILike(x.Region, $"%{cityQuery.Region}%"));
            }

            if (cityQuery.Page < 1)
            {
                cityQuery.Page = 1;
            }

            if (cityQuery.PageSize < 1)
            {
                cityQuery.PageSize = 1;
            }

            var count = await query.CountAsync();
            query = query.Skip((cityQuery.PageSize - 1) * cityQuery.PageSize).Take(cityQuery.PageSize);
            var items = await query.ToListAsync();

            return new PaginatedQueryResult<City>
            {
                Items = items,
                Count = count
            };
        }

        public async Task<IEnumerable<string>> GetRegionsAsync() => await _dbContext.Set<City>().AsQueryable().Select(x => x.Region).Distinct().ToListAsync();

        public async Task<IEnumerable<string>> GetUFsAsync() => await _dbContext.Set<City>().AsQueryable().Select(x => x.UF).Distinct().ToListAsync();
    }
}
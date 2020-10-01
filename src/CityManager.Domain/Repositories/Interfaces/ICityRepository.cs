using System.Collections.Generic;
using System.Threading.Tasks;
using CityManager.Domain.Entities;
using CityManager.Domain.Queries;
using CityManager.Domain.ValueObjects;

namespace CityManager.Domain.Repositories.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<PaginatedQueryResult<City>> GetAllAsync(CityQuery query);
        Task<IEnumerable<string>> GetRegionsAsync();
        Task<IEnumerable<string>> GetUFsAsync();
    }
}
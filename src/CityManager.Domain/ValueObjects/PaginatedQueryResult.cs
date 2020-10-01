using System.Collections.Generic;

namespace CityManager.Domain.ValueObjects
{
    public class PaginatedQueryResult<TEntity>
    {

        public IEnumerable<TEntity> Items { get; set; }

        public int Count { get; set; }

    }
}
using System.Threading.Tasks;
using CityManager.Domain.Repositories.Interfaces;

namespace CityManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CityManagerContext context;

        public UnitOfWork(CityManagerContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}
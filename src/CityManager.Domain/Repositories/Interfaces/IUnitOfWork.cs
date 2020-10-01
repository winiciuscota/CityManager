using System;
using System.Threading.Tasks;

namespace CityManager.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

        void Complete();
    }
}
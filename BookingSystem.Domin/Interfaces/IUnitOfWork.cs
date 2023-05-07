
using BookingSystem.Domin.Entities;

namespace BookingSystem.Domin.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}


using BookingSystem.Domin.Entities;

namespace BookingSystem.Domin.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetEntityByIdAsync(params object[] keyValues);
        IReadOnlyList<T> GetAll();
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        int GetCountWithSpec(ISpecifications<T> specifications);
        T? GetEntityWithSpec(ISpecifications<T> specifications);
        IReadOnlyList<T> GetAllWithSpec(ISpecifications<T> specifications);
    }
}

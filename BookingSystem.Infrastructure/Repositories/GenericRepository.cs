
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Domin.Specification;
using  BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace  BookingSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _context;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IReadOnlyList<T> GetAll()
            => _context.Set<T>().AsNoTracking().ToList();

        public async Task<T?> GetEntityByIdAsync(params object[] idValues)
            => await _context.Set<T>().FindAsync(idValues);

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public int GetCountWithSpec(ISpecifications<T> specifications)
            => ApplySpecifications(specifications, true).Count();

        public T? GetEntityWithSpec(ISpecifications<T> specifications)
            =>  ApplySpecifications(specifications).FirstOrDefault();

        public IReadOnlyList<T> GetAllWithSpec(ISpecifications<T> specifications)
            =>  ApplySpecifications(specifications).ToList();

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications, bool isCount = false)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications, isCount);
    }
}

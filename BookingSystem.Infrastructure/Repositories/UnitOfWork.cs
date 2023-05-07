using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using  BookingSystem.Infrastructure.Data;
using System.Collections;

namespace  BookingSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> Complete() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}

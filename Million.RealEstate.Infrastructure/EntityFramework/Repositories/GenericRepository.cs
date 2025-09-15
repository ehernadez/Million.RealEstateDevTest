using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly RealEstateDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(RealEstateDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllPaginateAsync(Expression<Func<T, bool>>? filter, int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>>? includeQuery = null)
        {
            var query = _dbSet.AsQueryable();

            if (includeQuery != null)
                query = includeQuery(query);

            if (filter != null)
                query = query.Where(filter);

            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            return (items, total);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}

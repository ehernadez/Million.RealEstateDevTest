using System.Linq.Expressions;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<(IEnumerable<T> Items, int TotalCount)> GetAllPaginateAsync(Expression<Func<T, bool>>? filter, int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>>? includeQuery = null);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
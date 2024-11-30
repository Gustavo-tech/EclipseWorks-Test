using System.Linq.Expressions;

namespace EclipseTest.Infrastructure.Interfaces;

public interface IRepository<T>
{
    Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

using System.Linq.Expressions;

namespace Movilissa.core.Interfaces.IServices;

public interface IGenericService<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes);

    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    
    Task Atomic(Func<Task> operation);
}
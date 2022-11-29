using System.Linq.Expressions;

namespace TodoBackend.Repositories;

public interface IRepository<T> where T : class
{
    T? GetById(int id);
    IEnumerable<T> Get();
    IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    void Insert(T entity);
    void Delete(int todoId);
    void Update(T entity);
}

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TodoBackend.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Db;
 
    //Normally Repository works only with UnitOfWork!
    //this is only for demonstration purpose, such that this repository
    //also works without a UnitOfWork.  
 
    public Repository(DbContext db)
    {
        Db = db;
    }
 
    //exposes IQueryable interface to outside world (with a public access modifier)
    //is not a good idea! 
    protected IQueryable<T> AsQueryable()
    {
        return Db.Set<T>().AsNoTracking();
    }
 
    public virtual T? GetById(int id)
    {
        return Db.Set<T>().Find(id);
    }
 
    public virtual IEnumerable<T> Get()
    {
        return Db.Set<T>().AsNoTracking().AsEnumerable();
    }
 
    public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return Db.Set<T>()
            .Where(predicate)
            .AsEnumerable();
    }
 
    public void Insert(T entity)
    {
        Db.Set<T>().Add(entity);
        Db.SaveChanges();
    }

    public void Delete(int todoId)
    {
        var itemTodDelete = GetById(todoId);
        if (itemTodDelete is not null)
        {
            Db.Set<T>().Remove(itemTodDelete);
            Db.SaveChanges();
        }
    }

    public void Update(T entity)
    {
        Db.Entry(entity).State = EntityState.Modified;
        Db.SaveChanges();
    }
 
    public void Delete(T entity)
    {
        Db.Set<T>().Remove(entity);
        Db.SaveChanges();
    }
}
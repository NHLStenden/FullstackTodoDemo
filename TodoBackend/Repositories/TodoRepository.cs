using Microsoft.EntityFrameworkCore;
using TodoBackend.Entities;

namespace TodoBackend.Repositories;

public class TodoRepository : Repository<Todo>, ITodoRepository
{
    private readonly TodoContext _db;

    public TodoRepository(TodoContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<Todo> GetCompleted()
    {
        return _db.Todos.Where(x => x.Completed)
            .Include(x => x.Category)
            .AsNoTracking().ToList();
    }

    public IEnumerable<Todo> GetWithCategories()
    {
        return _db.Todos.Include(x => x.Category)
            .AsNoTracking().ToList();
    }

    public IEnumerable<Todo> GetWithCategories(int categoryId)
    {
        return _db.Todos.Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .AsNoTracking().ToList();
    }

    public IEnumerable<Todo> GetWithCategories(string categoryName)
    {
        return _db.Todos
            .Include(x => x.Category)
            .Where(x => x.Category.Description == categoryName)
            .AsNoTracking().ToList();
    }

    public IEnumerable<Todo> GetWithCategoryBySlug(string slug)
    {
        return _db.Todos.Include(x => x.Category)
            .Where(x => x.Category.Slug == slug)
            .AsNoTracking().ToList();
    }

    public Todo GetByIdWithCategory(int todoId)
    {
        return _db.Todos
            .Include(x => x.Category)
            .SingleOrDefault(x => x.TodoId == todoId);
    }
}
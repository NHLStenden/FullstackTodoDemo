using Microsoft.EntityFrameworkCore;
using TodoBackend.Entities;

namespace TodoBackend.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly TodoContext _db;

    public CategoryRepository(TodoContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<Todo> GetByCategoryId(int categoryId)
    {

        return _db.Todos
            .Where(x => x.CategoryId == categoryId)
            .Include(x => x.Category)
            .AsNoTracking().ToList();
    }

    public IEnumerable<Todo> GetByCategory(string name)
    {
        var result= _db.Todos
            .Where(x => x.Description == name)
            .Include(x => x.Category)
            .AsNoTracking().ToList();
        return result;
    }

    public IEnumerable<Category> GetCategoryWithTodos()
    {
        return _db.Categories.
            Include(x => x.Todos.Take(2)).ToList();
    }

    public Category? FindCategoryByName(string name)
    {
        return _db.Categories.FirstOrDefault(x => x.Description == name);
    }
}
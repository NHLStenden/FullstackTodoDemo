using TodoBackend.Entities;

namespace TodoBackend.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Todo> GetByCategoryId(int categoryId);

    IEnumerable<Todo> GetByCategory(string name);

    Category? FindCategoryByName(string name);

    public IEnumerable<Category> GetCategoryWithTodos();
}
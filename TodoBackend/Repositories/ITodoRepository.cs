using TodoBackend.Entities;

namespace TodoBackend.Repositories;

public interface ITodoRepository : IRepository<Todo>
{
    IEnumerable<Todo> GetCompleted();

    IEnumerable<Todo> GetWithCategories();
    
    IEnumerable<Todo> GetWithCategories(int categoryId);
    
    Todo GetByIdWithCategory(int categoryCategoryId);
    
    IEnumerable<Todo> GetWithCategories(string categoryName);
    IEnumerable<Todo> GetWithCategoryBySlug(string slug);
}
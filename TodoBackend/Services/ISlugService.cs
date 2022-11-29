using TodoBackend.Entities;

namespace TodoBackend.Services;

public interface ISlugService
{
    public string GetSlug(string description);
    IEnumerable<Todo> GetTodosBySlug(string categoryName);
}
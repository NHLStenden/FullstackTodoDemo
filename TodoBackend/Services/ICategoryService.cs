using TodoBackend.Entities;

namespace TodoBackend.Services;

public interface ICategoryService
{
    Category Create(Category category);
}
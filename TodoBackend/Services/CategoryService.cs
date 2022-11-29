using TodoBackend.Entities;
using TodoBackend.Repositories;

namespace TodoBackend.Services;

public class CategoryService : ICategoryService
{
    private readonly ISlugService _slugService;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ISlugService slugService, ICategoryRepository categoryRepository)
    {
        _slugService = slugService;
        _categoryRepository = categoryRepository;
    }
    
    public Category Create(Category category)
    {
        category.Slug = _slugService.GetSlug(category.Description);
        _categoryRepository.Insert(category);

        var insertedCategory = _categoryRepository.GetById(category.CategoryId);
        return insertedCategory!;
    }
}
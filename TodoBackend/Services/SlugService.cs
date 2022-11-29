using AutoMapper;
using Slugify;
using TodoBackend.Entities;
using TodoBackend.Repositories;

namespace TodoBackend.Services;

public class SlugService : ISlugService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public SlugService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public string GetSlug(string description)
    {
        SlugHelper slugHelper = new SlugHelper();
        return slugHelper.GenerateSlug(description);
    }

    public IEnumerable<Todo> GetTodosBySlug(string categoryName)
    {
        SlugHelper slugHelper = new SlugHelper();
        string slug = slugHelper.GenerateSlug(categoryName);

        var todos = _todoRepository.GetWithCategoryBySlug(slug);

        return todos;
    }
}
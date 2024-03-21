using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.Entities;
using TodoBackend.Repositories;
using TodoBackend.Requests;
using TodoBackend.Responses;
using TodoBackend.Services;

namespace TodoBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly ISlugService _slugService;

    public CategoryController(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ICategoryService categoryService, ISlugService slugService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _categoryService = categoryService;
        _slugService = slugService;
    }
    
    [HttpGet("{categoryId:int}/todos")]
    public ActionResult<IEnumerable<TodoCategoryResponse>> GetTodosByCategoryId(int categoryId)
    {
        var result = _categoryRepository.GetByCategoryId(categoryId); 
        return Ok(result);
    }

    [HttpGet("{slug}/todos")]
    public ActionResult<IEnumerable<TodoCategoryResponse>> GetTodosBySlug(string slug)
    {
        var todos = _slugService.GetTodosBySlug(slug);
        var result = _mapper.Map<IEnumerable<TodoCategoryResponse>>(todos);
        
        return Ok(result);
    }

    [HttpGet(nameof(GetById))]
    public ActionResult<Category?> GetById(int id)
    {
        var result = _categoryRepository.GetById(id);
        if (result is null)
            return NotFound();
        
        return result;
    }

    [HttpGet(nameof(Get))]
    public IEnumerable<Category> Get()
    {
        return _categoryRepository.Get();
    }

    [HttpPost]
    public ActionResult<TodoCategoryResponse> Create(CategoryRequest categoryRequest)
    {
        var category = _mapper.Map<Category>(categoryRequest);

        var result = _categoryService.Create(category);
        
        return CreatedAtAction(
            nameof(GetById), new {categoryId = result.CategoryId}, category);
    }

    [HttpGet(nameof(GetCategoryWithTodos))]
    public IEnumerable<Category> GetCategoryWithTodos()
    {
        return _categoryRepository.GetCategoryWithTodos();
    }
}
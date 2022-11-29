using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.Entities;
using TodoBackend.Repositories;
using TodoBackend.Responses;
using TodoBackend.Services;

namespace TodoBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private IMyLogger _logger;
    private readonly ITodoRepository _todoRepository;
    private readonly ISlugService _slugService;
    private readonly IMapper _mapper;

    public TodoController(IMyLogger logger, 
        ITodoRepository todoRepository, 
        ISlugService slugService,
        IMapper mapper)
    {
        _logger = logger;
        _todoRepository = todoRepository;
        _slugService = slugService;
        _mapper = mapper;
    }

    [HttpGet(nameof(GetAll))]
    public IEnumerable<Todo> GetAll()
    {
        _logger.Log("GetAll()");
        return _todoRepository.Get();
    }
    
    [HttpGet(nameof(GetAllCompleted))]
    public ActionResult<IEnumerable<Todo>> GetAllCompleted()
    {
        // var todoCompleted = _db.Todos
        //     .Where(x => x.Completed)
        //     .AsNoTracking()
        //     .ToList();

        var result = _todoRepository.GetCompleted(); 
        return Ok(result);
    }

    [HttpGet(nameof(GetTodosWithCategories))]
    public ActionResult<IEnumerable<TodoCategoryResponse>> GetTodosWithCategories()
    {
        var todosWithCategory = _todoRepository.GetWithCategories();
        var result = _mapper.Map<IEnumerable<TodoCategoryResponse>>(todosWithCategory);
        return Ok(result);
    }

    [HttpGet(nameof(GetById))]
    public ActionResult<Todo> GetById(int todoId)
    {
        var todo = _todoRepository.GetById(todoId);
        if (todo is null)
        {
            _logger.Log("GetById() - Not Found");
            return NotFound();
        }

        return  Ok(todo);
    }

    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        _todoRepository.Insert(todo);
        var insertedTodo = _todoRepository.GetById(todo.TodoId);

        return CreatedAtAction(
            nameof(GetById), new {todoId = todo.TodoId}, insertedTodo);
    }

    [HttpDelete]
    public ActionResult Delete(int todoId)
    {
        _todoRepository.Delete(todoId);
        return Ok();
    }

    [HttpPut]
    public ActionResult<Todo> Put(int todoId, Todo todo)
    {
        if (todoId != todo.TodoId)
        {
            throw new ArgumentException();
        }
        
        _todoRepository.Update(todo);
        var updatedEntity = _todoRepository.GetById(todoId);
        if (updatedEntity is not null)
        {
            return updatedEntity;
        }

        return NotFound();
    }
    
    [HttpGet(nameof(GetByCategoryDescription))]
    public ActionResult<IEnumerable<Todo>> GetByCategoryDescription(string description)
    {
        var todos = _todoRepository.GetWithCategories(description);

        var result = _mapper.Map<IEnumerable<TodoCategoryResponse>>(todos);
        
        return Ok(result);
    }
}
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TonicTodoApi.Models;
using TonicTodoApi.Repositories;

namespace TonicTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger _logger;//TODO: Add logging, authentication, exception handling
        private readonly ITodoRepository _todoRepository;
        private IValidator<Todo> _validator;

        public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepository, IValidator<Todo> validator)
        {
            _logger = logger;
            _todoRepository = todoRepository;
            _validator = validator;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            var todos = await _todoRepository.GetAllTodosAsync();

            if (todos.IsNullOrEmpty())
                return NotFound();

            return Ok(todos);
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name="GetTodoByIdAsync")]
        public async Task<ActionResult<Todo>> GetTodoByIdAsync(int id)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(id);

            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Todo todoNew)
        {
            ValidationResult result = await _validator.ValidateAsync(todoNew);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (todoNew is null)
            {
                return BadRequest("Todo cannot be null.");
            }

            var todo = await _todoRepository.GetTodoByIdAsync(todoNew.Id);

            if (todo is null)
                return NotFound();

            todo.Name = todoNew.Name;
            todo.IsComplete = todoNew.IsComplete;
            todo.Secret = todoNew.Secret;

            await _todoRepository.UpdateAsync(todo);

            return NoContent();
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
        {
            ValidationResult result = await _validator.ValidateAsync(todo);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await _todoRepository.CreateAsync(todo);

            return CreatedAtRoute("GetTodoByIdAsync", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(id);

            if (todo is null)
                return NotFound();

            await _todoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TonicTodoApi.Data;
using TonicTodoApi.Models;

namespace TonicTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger _logger;
        //private readonly ITodoRepository _todoRepository;
        private readonly TodoDbContext _context;

        //public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepository)
        public TodoController(ILogger<TodoController> logger, TodoDbContext context)
        {
            _logger = logger;
            //_todoRepository = todoRepository;
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            //var todos = await _todoRepository.GetAllTodosAsync();
            var todos = await _context.Todos.ToListAsync();

            if (todos.IsNullOrEmpty())
                return NotFound();

            return Ok(todos);
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name="GetTodoByIdAsync")]
        public async Task<ActionResult<Todo>> GetTodoByIdAsync(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetTodoByIdAsync", new { id = todo.Id }, todo);
            //return CreatedAtAction("GetTodoByIdAsync", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}

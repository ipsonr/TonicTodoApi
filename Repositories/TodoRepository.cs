using TonicTodoApi.Data;
using TonicTodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TonicTodoApi.Repositories
{
    public class TodoRepository(TodoDbContext dbContext) : ITodoRepository
    {
        private readonly TodoDbContext _dbContext = dbContext;

        public async Task<ActionResult> CreateAsync(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetTodoByIdAsync), "Todo", new { id = todo.Id }, todo);
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _dbContext.Todos.FindAsync(id) is Todo todo)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return new NoContentResult();
            }

            return new NotFoundResult();
        }

        public async Task<List<Todo>> GetCompletedTodosAsync()
        {
            var completedTodos = await _dbContext.Todos.Where(t => t.IsComplete).ToListAsync();
            return completedTodos;
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);

            if (todo is null)
                throw new Exception("Todo not found");

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task<ActionResult> UpdateAsync(Todo inputTodo)
        {
            var todo = await _dbContext.Todos.FindAsync(inputTodo.Id);

            if (todo is null) 
                return new NotFoundResult();

            todo.Name = inputTodo.Name;
            todo.IsComplete = inputTodo.IsComplete;
            todo.Secret = inputTodo.Secret;

            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(todo);
        }
    }
}

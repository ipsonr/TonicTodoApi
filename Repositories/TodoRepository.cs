using TonicTodoApi.Data;
using TonicTodoApi.Models;
using TonicTodoApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TonicTodoApi.Repositories
{
    public class TodoRepository(TodoDbContext dbContext) : ITodoRepository
    {
        private readonly TodoDbContext _dbContext = dbContext;

        //public TodoRepository(TodoDbContext dbContext)//use primary constructor now
        //{
        //    _dbContext = dbContext;
        //}
        public async Task<ActionResult<Todo>> Create(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();

            //return TypedResults.Created($"/todoitems/{todo.Id}", todo);
            return CreatedAtAction(nameof(GetTodoByIdAsync), new { id = todo.Id }, todo);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (await _dbContext.Todos.FindAsync(id) is Todo todo)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }

        public async Task<IEnumerable<Todo>> GetCompletedTodosAsync()
        {
            return TypedResults.Ok(await _dbContext.Todos.Where(t => t.IsComplete).ToListAsync());
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
            var todos = _dbContext.Todos.ToList();
            return TypedResults.Ok(await _dbContext.Todos.ToArrayAsync());
            //return _dbContext.Todos.ToList();
            //return Ok(await _dbContext.Todos.ToListAsync());
        }

        //public async Task<ActionResult> Update(Todo inputTodo)
        public async Task Update(Todo inputTodo)
        {
            var todo = await _dbContext.Todos.FindAsync(inputTodo.Id);

            if (todo is null) //return TypedResults.NotFound();
                throw new Exception("Todo not found");

            todo.Name = inputTodo.Name;
            todo.IsComplete = inputTodo.IsComplete;

            await _dbContext.SaveChangesAsync();

            //return TypedResults.NoContent();
        }
    }
}

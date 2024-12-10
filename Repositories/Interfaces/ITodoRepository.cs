using Microsoft.AspNetCore.Mvc;
using TonicTodoApi.Models;

namespace TonicTodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> GetTodoByIdAsync(int id);
        Task<IEnumerable<Todo>> GetAllTodosAsync();
        Task<List<Todo>> GetCompletedTodosAsync();
        Task<ActionResult> DeleteAsync(int id);
        Task<ActionResult> CreateAsync(Todo todo);
        Task<ActionResult> UpdateAsync(Todo todo);
    }
}

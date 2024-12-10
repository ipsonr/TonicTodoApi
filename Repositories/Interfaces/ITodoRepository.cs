using Microsoft.AspNetCore.Mvc;
using TonicTodoApi.Models;

namespace TonicTodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<ActionResult> GetTodoByIdAsync(int id);
        Task<ActionResult> GetAllTodosAsync();
        Task<ActionResult> GetCompletedTodosAsync();
        Task<ActionResult> DeleteAsync(int id);
        Task<ActionResult> CreateAsync(Todo todo);
        Task<ActionResult> UpdateAsync(Todo todo);
    }
}

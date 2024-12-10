using Microsoft.AspNetCore.Mvc;
using TonicTodoApi.Models;

namespace TonicTodoApi.Services
{    public interface ITodoService
    {
        Task<ActionResult> Create(Todo todo);
        Task<ActionResult> Delete(int id);
        Task<ActionResult> GetCompletedTodosAsync();

        Task<ActionResult> GetTodoAsync(int id);

        Task<ActionResult> GetTodosAsync();

        Task<ActionResult> Update(Todo inputTodo);
        //Task<IResult> Create(Todo todo);
        //Task<IResult> Delete(int id);
        //Task<IResult> GetCompletedTodosAsync();

        //Task<IResult> GetTodoAsync(int id);

        //Task<IResult> GetTodosAsync();

        //Task<IResult> Update(Todo inputTodo);
    }
}

using Microsoft.AspNetCore.Mvc;
using TonicTodoApi.Models;
using TonicTodoApi.Repositories;

namespace TonicTodoApi.Services
{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        private readonly ITodoRepository _todoRepository = todoRepository;

        public async Task<ActionResult> Create(Todo todo)
        {
            //var validationResult = validate(todo);
            //if (validationResult == false) {

            return await _todoRepository.Create(todo);
        }

        public Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> GetCompletedTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> GetTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> GetTodosAsync()
        {
            return await _todoRepository.GetAllTodosAsync();
        }

        public Task<ActionResult> Update(Todo inputTodo)
        {
            throw new NotImplementedException();
        }
    }
}

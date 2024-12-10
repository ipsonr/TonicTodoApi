using Microsoft.EntityFrameworkCore;
using TonicTodoApi.Data;

namespace TonicTodoApi.Repositories.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod()]
        public async Task CreateAsyncTest()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
           .UseInMemoryDatabase(databaseName: "Tonic")
           .Options;

            using (var context = new TodoDbContext(options))
            {
                context.Todos.Add(new Models.Todo
                {
                    Name = "Test1",
                    IsComplete = false,
                    Secret = "Secret1"
                });
                context.Todos.Add(new Models.Todo
                {
                    Name = "Test2",
                    IsComplete = true,
                    Secret = "Secret sauce"
                });
                context.Todos.Add(new Models.Todo
                {
                    Name = "Test4",
                    IsComplete = true,
                    Secret = "Secret4"
                });
                context.SaveChanges();
            }

            using (var context = new TodoDbContext(options))
            {
                TodoRepository todoRepository = new TodoRepository(context);

                var todos = await todoRepository.GetAllTodosAsync();

                Assert.AreEqual(3, todos.Count());
            }
        }
    }
}
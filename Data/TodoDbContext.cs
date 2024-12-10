using TonicTodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TonicTodoApi.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Tonic");
        }
        //protected readonly IConfiguration Configuration;

        //public TodoDbContext(DbContextOptions<TodoDbContext> options, IConfiguration configuration) : base(options)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(Configuration.GetConnectionString("TodoConnectionString"));
        //}

        public DbSet<Todo> Todos => Set<Todo>();
    }
}

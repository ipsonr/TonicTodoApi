using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TonicTodoApi.Data;
using TonicTodoApi.Repositories;
using TonicTodoApi.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<TodoValidator>();

//var connectionString =
//    builder.Configuration.GetConnectionString("TodoConnectionString")
//        ?? throw new InvalidOperationException("Connection string"
//        + "'TodoConnectionString' not found.");

////builder.Services.AddDbContext<TodoDbContext>(options =>
//  //  options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<TodoDbContext>(options =>
//    options.UseSqlServer(connectionString, options =>
//    {
//        options.EnableRetryOnFailure();
//    }));

#if DEBUG
{
    builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("Tonic"));//use for testing if no db available
    //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}
#endif


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

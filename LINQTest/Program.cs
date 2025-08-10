var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();

//LINQ collection types must implement the IQueryable or generic IQueryable interface
// or the Innumerable or generic Innumerable interface 
//IQueryable interface inherits from the IEnumerable interface and the
//generic IQueryable interface inherits from both the IEnumerable interface and the
//generic IEnumerable interface


//Deferred executions is evaluation of an expression is delayed until the value is required 
// It re-evaluates on each execution which is known as lazy evaluation
using EmptyStack.Data;
using EmptyStack.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmptyStackDb>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=emptystack;Username=postgres;Password=73Merlin37"));
var app = builder.Build();

app.UseRouting();
app.MapGroup("/questions").MapQuestions();

app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.Data;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
//Regist some services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaContext>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();
//Configurre HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});


//Configure routes
app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas", async (PizzaContext db) => await db.Pizzas.ToListAsync());

app.MapPost("/pizza", async (PizzaContext db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.Run();

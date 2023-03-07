using GameStore.API.Data;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//using var dbContext = builder.ApplicationServices.GetService<GameStoreContext>();
//dbContext?.Database.Migrate();

// GET /games
app.MapGet("/games", async (GameStoreContext context)
    => await context.Games.AsNoTracking().ToListAsync());

// PUT /games/{id}
app.MapPut("/games/{id}", async (int id, Game updatedGame, GameStoreContext context) =>
{
    /*var rowsAffected = await context.Games.Where(game => game.Id == id)
        .ExecuteUpdateAsync(updates => 
            updates.SetProperty(game => game.Name, updatedGame.Name)
                   .SetProperty(game => game.Price, updatedGame.Price)
                   .SetProperty(game => game.ReleaseDate, updatedGame.ReleaseDate));*/

    return Results.NoContent();
});

// DELETE /games/{id}
app.MapDelete("/games/{id}", async (int id, GameStoreContext context) =>
{
    /*var rowsAffected = await context.Games.Where(game => game.Id == id)
                            .ExecuteDeleteAsync();*/

    //return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();

    return Results.NoContent();
});

app.Run();

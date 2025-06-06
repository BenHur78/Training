namespace GameStore.API.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }
}
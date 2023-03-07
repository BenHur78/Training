using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Data;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas { get; set; } = null!;
}
using Microsoft.EntityFrameworkCore;

namespace TodoBackend.Entities;

public class TodoContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public int TodoId { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public bool Completed { get; set; }
    public DbSet<Category?> Categories { get; set; }
    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
namespace TodoBackend.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Description { get; set; }
    
    public string Slug { get; set; }

    public List<Todo> Todos { get; set; } = new List<Todo>();
}
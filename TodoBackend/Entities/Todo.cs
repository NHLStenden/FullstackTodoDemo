namespace TodoBackend.Entities;

public class Todo
{
    public int TodoId { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }


    
}
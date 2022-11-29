namespace TodoBackend.Responses;

public class TodoCategoryResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Completed { get; set; }
    public string Category{ get; set; }
    
    public string Slug { get; set; }
}
namespace Infrastructure.DataAccess.Models;

public class Note
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Text { get; set; }
    
    public DateTime Created { get; internal set; }
    
    public DateTime Updated { get; internal set; }
}
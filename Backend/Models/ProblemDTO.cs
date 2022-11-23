namespace Backend.Models;

public class ProblemDTO
{
    public int ProblemId { get; set; }
    
    public int UserId { get; set; }
    
    public string Title { get; set; } = default!;
    
    public List<ProDTO>? ProList { get; set; }
    
    public List<ConsDTO>? ConList { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Problem
{
    public int ProblemId { get; set; }
    
    public User User { get; set; } = default!;
    
    public string Title { get; set; } = default!;
    
    public List<Pro>? ProList { get; set; }
    
    public List<Con>? ConList { get; set; }
}
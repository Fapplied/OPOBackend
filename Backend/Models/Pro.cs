using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Pro
{
    public int ProId { get; set; }
    
    public int UserId { get; set; }

    public string Title { get; set; } = default!;
    
    public int ProblemId { get; set; } = default!;
    public Problem Problem { get; set; } = default!;

    public List<ProLike>? LikesList { get; set; }
}
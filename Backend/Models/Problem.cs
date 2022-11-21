using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Problem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProblemId { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    public string Title { get; set; } = default!;
    
    public List<Pro> ProList { get; set; } = default!;
    
    public List<Con> Conlist { get; set; } = default!;
}
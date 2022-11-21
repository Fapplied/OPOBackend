using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models;

public class Pro
{
   
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ProId { get; set; } 

// [Required]
// [ForeignKey("Problem")]
// public int ProblemId { get; set; } = default!;

[Required] 
public string Title { get; set; } = default!;

// [NotMapped]
// public List<ProLike> Likes { get; set; } = default!;

public List<userId>? LikesList { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models;

public class Con
{
   
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ConId { get; set; } 

[ForeignKey("Problem")]
public int ProblemId { get; set; }

[Required]
public string Title { get; set; }

[NotMapped]
public List<ConLike> Likes { get; set; } = default!;

}

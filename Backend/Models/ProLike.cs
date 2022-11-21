using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;
[Keyless]
public class ProLike
{   
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    [ForeignKey("Pro")]
    public int ProId { get; set; }

}
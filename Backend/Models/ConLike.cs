using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;
[Keyless]
public class ConLike
{   
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    [ForeignKey("Con")]
    public int ConId { get; set; }

}
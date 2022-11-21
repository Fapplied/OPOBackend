using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;
[Keyless]
public class ProfilePicture
{
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    public string Url { get; set; }
}
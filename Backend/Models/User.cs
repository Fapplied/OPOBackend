using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models;

public class User
{
   
public string Name { get; set; } 

[Required]
[Key]
public int UserId { get; set;}  = default!;

}

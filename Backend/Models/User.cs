using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class User
{
    public int UserId { get; set; } = default!;

    public string Name { get; set; }

    public ProfilePicture? ProfilePicture { get; set; }

    public List<Problem>? ProblemList { get; set; }
}
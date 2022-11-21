using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class ProfilePicture
{
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }
}
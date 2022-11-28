using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Backend.Data;


public class OPODB:DbContext
{
    
    public OPODB(DbContextOptions<OPODB> options)
        : base(options)
    {
    }
    
    public Microsoft.EntityFrameworkCore.DbSet<User> User { get; set; } = default!;
    public Microsoft.EntityFrameworkCore.DbSet<ProfilePicture> ProfilePicture { get; set; } = default!;
    public Microsoft.EntityFrameworkCore.DbSet<Problem> Problem { get; set; } = default!;
    public Microsoft.EntityFrameworkCore.DbSet<Pro> Pro { get; set;  }
    public Microsoft.EntityFrameworkCore.DbSet<Con> Con { get; set;  }
    public Microsoft.EntityFrameworkCore.DbSet<ProLike> ProLike { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<ConLike> ConLike { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Problem>()
            .HasMany(a => a.ProList).WithOne(a => a.Problem)
            .HasForeignKey(a => a.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Pro>()
            .HasMany(a => a.LikesList).WithOne(a => a.Pro)
            .HasForeignKey(a => a.ProId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Con>()
            .HasMany(a => a.LikesList).WithOne(a => a.Con)
            .HasForeignKey(a => a.ConId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    
    
    
    
}
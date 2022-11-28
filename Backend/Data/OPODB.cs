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
    // public DbSet<ProLike> ProLike { get; set;  }
    // public DbSet<ConLike> ConLike { get; set;  }
    public Microsoft.EntityFrameworkCore.DbSet<Pro> Pro { get; set;  }
    public Microsoft.EntityFrameworkCore.DbSet<Con> Con { get; set;  }
    public Microsoft.EntityFrameworkCore.DbSet<Like> Likes { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder builder)
    // {   
    //     builder.Entity<Problem>()
    //         .HasOptional(a => a.UserDetail)
    //         .WithOptionalDependent()
    //         .WillCascadeOnDelete(true);
    // }
}
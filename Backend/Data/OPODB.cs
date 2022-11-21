using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;


public class OPODB:DbContext
{
    
    public OPODB(DbContextOptions<OPODB> options)
        : base(options)
    {
    }
    


    public DbSet<User> User { get; set; } = default!;
    public DbSet<ProfilePicture> ProfilePicture { get; set; } = default!;
    public DbSet<Problem> Problem { get; set; } = default!;
    public DbSet<ProLike> ProLike { get; set;  }
    public DbSet<ConLike> ConLike { get; set;  }
    public DbSet<Pro> Pro { get; set;  }
    public DbSet<Con> Con { get; set;  }
}
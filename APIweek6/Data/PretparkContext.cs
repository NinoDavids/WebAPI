using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;
using APIweek6.Models;
using Microsoft.AspNetCore.Identity;

public class PretparkContext : IdentityDbContext<User.ApplicationUser>
    {
        public PretparkContext (DbContextOptions<PretparkContext> options)
            : base(options)
        {
        }
        public DbSet<Attractie> Attractie { get; set; } = default!;
        
    }

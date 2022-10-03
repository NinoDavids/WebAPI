using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;

    public class PretparkContext : IdentityDbContext
    {
        public PretparkContext (DbContextOptions<PretparkContext> options)
            : base(options)
        {
        }

        public DbSet<API.Models.Attractie> Attractie { get; set; } = default!;
    }

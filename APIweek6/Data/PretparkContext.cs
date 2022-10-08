using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;
using APIweek6.Models;
using Microsoft.AspNetCore.Identity;

namespace APIweek6.Data
{
    public class PretparkContext : IdentityDbContext<User>
    {
        public PretparkContext (DbContextOptions<PretparkContext> options)
            : base(options)
        {
        }
        public DbSet<Attractie> Attractie { get; set; } = default!;
        public DbSet<LikedAttractie> LikedAttractie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<LikedAttractie>()
                .HasKey(bc => new { bc.AttractieId, bc.UserId });  

            modelBuilder.Entity<LikedAttractie>()
                .HasOne(bc => bc.Attractie)
                .WithMany(b => b.LikedAttracties)
                .HasForeignKey(bc => bc.AttractieId);  

            modelBuilder.Entity<LikedAttractie>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.LikedAttracties)
                .HasForeignKey(bc => bc.UserId);
        }
    }
    
    
}


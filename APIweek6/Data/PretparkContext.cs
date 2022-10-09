using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Medewerker",
                    NormalizedName = "MEDEWERKER"
                },
                new IdentityRole
                {
                    Name = "Gast",
                    NormalizedName = "GAST"
                }
            );
            
            modelBuilder.Entity<Attractie>().HasData(
                new Attractie
                {
                    Id = 1,
                    name = "Reuzenrat",
                    spooky = 5,
                    buildYeaar = new DateTime(2001, 11, 13)
                },
                new Attractie
                {
                    Id = 2,
                    name = "Splash",
                    spooky = 20,
                    buildYeaar = new DateTime(2005, 5, 1)
                },
                new Attractie
                {
                    Id = 3,
                    name = "Spookhuis",
                    spooky = 60,
                    buildYeaar = new DateTime(666, 1, 25)
                },
                new Attractie
                {
                    Id = 4,
                    name = "Airborne",
                    spooky = 50,
                    buildYeaar = new DateTime(2002, 2, 13)
                },
                new Attractie
                {
                    Id = 5,
                    name = "Babyflug",
                    spooky = 5,
                    buildYeaar = new DateTime(1999, 8, 17)
                },
                new Attractie
                {
                    Id = 6,
                    name = "Draaimolen",
                    spooky = 5,
                    buildYeaar = new DateTime(1985, 9, 23)
                },
                new Attractie
                {
                    Id = 7,
                    name = "Huricane",
                    spooky = 45,
                    buildYeaar = new DateTime(2008, 4, 30)
                },
                new Attractie
                {
                    Id = 8,
                    name = "Tea cups",
                    spooky = 10,
                    buildYeaar = new DateTime(2019, 3, 31)
                },
                new Attractie
                {
                    Id = 9,
                    name = "Pusher",
                    spooky = 80,
                    buildYeaar = new DateTime(2020, 9, 10)
                },
                new Attractie
                {
                    Id = 10,
                    name = "Rups",
                    spooky = 20,
                    buildYeaar = new DateTime(1996, 11, 12)
                },
                new Attractie
                {
                    Id = 11,
                    name = "Cake Walk",
                    spooky = 0,
                    buildYeaar = new DateTime(1998, 1, 8)
                },
                new Attractie
                {
                    Id = 12,
                    name = "Toxic",
                    spooky = 95,
                    buildYeaar = new DateTime(2016, 4, 13)
                }
            );
        }
    }
    
    
}


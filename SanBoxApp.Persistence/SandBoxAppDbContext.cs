using Microsoft.EntityFrameworkCore;
using SandBoxApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanBoxApp.Persistence
{
    public class SandBoxAppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public SandBoxAppDbContext(DbContextOptions<SandBoxAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id =1,
                    Name = "User",
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin",
                });
        }
    }
}

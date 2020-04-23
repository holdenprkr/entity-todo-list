using System;
using System.Collections.Generic;
using System.Text;
using Identity_Todo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity_Todo.Models.ViewModels;

namespace Identity_Todo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<TodoStatus> TodoStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Holden",
                LastName = "Parker",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create two cohorts
            modelBuilder.Entity<TodoStatus>().HasData(
                new TodoStatus()
                {
                    Id = 1,
                    Title = "Todo"
                },
                new TodoStatus()
                {
                    Id = 2,
                    Title = "In Progress"
                },
                new TodoStatus()
                {
                    Id = 3,
                    Title = "Done"
                }

            );
        }
    }
}

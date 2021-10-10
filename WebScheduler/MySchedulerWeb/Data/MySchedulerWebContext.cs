using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySchedulerWeb.Models;

namespace MySchedulerWeb.Data
{
    public class MySchedulerWebContext : DbContext
    {
        public MySchedulerWebContext (DbContextOptions<MySchedulerWebContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DayEvent> DayEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin";

            Role adminRole = new Role { id = 1, Name = adminRoleName };
            Role userRole = new Role { id = 2, Name = userRoleName };

            User adminUser = new User { Id = 1, UserName = "Admin", Email = adminEmail, Password = adminPassword, RoleId = adminRole.id};

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);

        }
    }
}

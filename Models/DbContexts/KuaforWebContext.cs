using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KuaforRandevu2.Models.Entities;

namespace KuaforRandevu2.Models.DbContexts
{
    public partial class KuaforWebContext : DbContext
    {
        public KuaforWebContext()
        {
        }

        public KuaforWebContext(DbContextOptions<KuaforWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactForm> ContactForm { get; set; }
        public virtual DbSet<Expert> Expert { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "admin",
                    FullName = "SB",
                    Gender = "Male",
                    Email = "sss@sss.com",
                    Phone = "5555555",
                    Address = "BURSA",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    UserName = "user",
                    Password = "1234",
                    FullName = "Suleymanb",
                    Gender = "Male",
                    Email = "sss@sss.com",
                    Phone = "5555555",
                    Address = "BURSA",
                    RoleId = 2
                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 1,
                    RoleName = "admin"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "user"
                });

        }
    }
}

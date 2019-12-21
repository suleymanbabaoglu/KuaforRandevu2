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
           
        }
    }
}

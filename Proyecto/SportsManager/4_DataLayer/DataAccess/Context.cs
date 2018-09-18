using BusinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.UserOID);
            modelBuilder.Entity<Team>().HasKey(t => t.TeamOID);
            modelBuilder.Entity<Sport>().HasKey(s => s.SportOID);
            modelBuilder.Entity<Event>().HasKey(e => e.EventOID);


            //modelBuilder.Entity<Teacher>().ToTable("Teachers");
            //modelBuilder.Entity<Student>().ToTable("Students");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SportsManagerDB;Trusted_Connection=True;");
        }
    }
}

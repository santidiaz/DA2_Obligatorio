using BusinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    internal class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.UserOID);
            modelBuilder.Entity<Team>().HasKey(t => t.TeamOID);


            //modelBuilder.Entity<Teacher>().ToTable("Teachers");
            //modelBuilder.Entity<Student>().ToTable("Students");
        }
    }
}

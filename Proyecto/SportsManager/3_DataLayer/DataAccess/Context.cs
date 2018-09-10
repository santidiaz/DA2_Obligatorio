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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.UserOID);
            
            //modelBuilder.Entity<Teacher>().ToTable("Teachers");
            //modelBuilder.Entity<Student>().ToTable("Students");
        }
    }
}

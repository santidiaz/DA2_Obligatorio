using BusinessEntities;
using BusinessEntities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("SportManagerCS"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasKey(t => t.TeamOID);
            modelBuilder.Entity<Comment>().HasKey(c => c.CommentOID);
            modelBuilder.Entity<User>().HasKey(u => u.UserOID);            
            modelBuilder.Entity<Sport>().HasKey(s => s.SportOID);
            modelBuilder.Entity<Event>().HasKey(e => e.EventOID);
            modelBuilder.Entity<Comment>().HasKey(e => e.CommentOID);

            modelBuilder.Entity<User>().HasData(new User()
            {
                Email = "aaa@bbb.com",
                Password = CommonUtilities.HashTool.GenerateHash("123456"),
                IsAdmin = true,
                Name = "defaultName",
                LastName = "defaultLastName",
                UserName = "superadmin",
                Token = Guid.NewGuid(),
                UserOID = 1
            });

            modelBuilder.Entity<UserTeam>().HasKey(ut => new { ut.UserOID, ut.TeamOID });
        }
    }
}

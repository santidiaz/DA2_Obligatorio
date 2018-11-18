using BusinessEntities;
using BusinessEntities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public DbSet<EventTeam> EventTeams { get; set; }
        public DbSet<EventResult> EventResults { get; set; }
        public DbSet<TeamResult> TeamResults { get; set; }

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
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);            
            modelBuilder.Entity<Sport>().HasKey(s => s.Id);
            modelBuilder.Entity<Event>().HasKey(e => e.Id);
            modelBuilder.Entity<EventResult>().HasKey(er => er.Id);
            modelBuilder.Entity<TeamResult>().HasKey(tr => tr.Id);

            // Join entities
            modelBuilder.Entity<UserTeam>().HasKey(ut => new { ut.UserId, ut.TeamId });
            modelBuilder.Entity<EventTeam>().HasKey(et => new { et.EventId, et.TeamId});

            // Default user
            modelBuilder.Entity<User>().HasData(new User()
            {
                Email = "aaa@bbb.com",
                Password = CommonUtilities.HashTool.GenerateHash("123456"),
                IsAdmin = true,
                Name = "defaultName",
                LastName = "defaultLastName",
                UserName = "superadmin",
                Token = Guid.NewGuid(),
                Id = 1
            });            
        }
    }
}

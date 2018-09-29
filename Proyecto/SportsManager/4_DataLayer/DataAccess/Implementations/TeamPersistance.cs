﻿using BusinessEntities;
using DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Implementations
{
    public class TeamPersistance : ITeamPersistance
    {
        public void AddTeam(Team newTeam)
        {
            using (Context context = new Context())
            {
                context.Teams.Add(newTeam);
                context.SaveChanges();
            }
        }

        public void DeleteTeamByName(Team teamToDelete)
        {
            using (Context context = new Context())
            {
                context.Teams.Attach(teamToDelete);
                context.Teams.Remove(teamToDelete);
                context.SaveChanges();
            }
        }

        public Team GetTeamByName(string name)
        {
            Team foundTeam;
            using (Context context = new Context())
            {
                foundTeam = context.Teams.OfType<Team>().FirstOrDefault(u => u.Name.Equals(name));
            }
            return foundTeam;
        }

        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            using (Context context = new Context())
            {
                teams = context.Teams.OfType<Team>().ToList();
            }
            return teams;
        }

        public bool IsTeamInSystem(Team team)
        {
            bool result = false;
            using (Context context = new Context())
            {
                var teamOnDB = context.Teams.OfType<Team>().Include("Teams").Where(a => a.TeamOID.Equals(team.TeamOID)).FirstOrDefault();

                result = teamOnDB != null ? true : false;
            }
            return result;
        }

        public void ModifyTeamByName(string name, Team teamToModify)
        {
            using (Context context = new Context())
            {
                var teamOnDB = context.Teams.OfType<Team>().Include("Teams").Where(a => a.TeamOID.Equals(teamToModify.TeamOID)).FirstOrDefault();
                
                teamOnDB.Name = teamToModify.Name;
                teamOnDB.Photo = teamToModify.Photo;

                context.SaveChanges();
            }
        }
    }
}

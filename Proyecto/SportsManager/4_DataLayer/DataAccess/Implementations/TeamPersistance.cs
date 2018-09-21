using BusinessEntities;
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

        public void DeleteTeamByName(string name)
        {
            using (Context context = new Context())
            {
                Team teamToDelete = new Team() { Name = name };
                context.Teams.Attach(teamToDelete);
                context.Teams.Remove(teamToDelete);
                context.SaveChanges();
            }
        }

        public Team GetTeamByName(string name)
        {
            Team teamFound;
            using (Context context = new Context())
            {
                var queryResult = (from team in (context.Teams).Include("Teams")
                                   where team.Name.Equals(name)
                                   select team).FirstOrDefault();

                teamFound = queryResult;
            }
            return teamFound;
        }

        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            using (Context context = new Context())
            {
                var query = from team in context.Teams.Include("Teams")
                            select team;

                if (query != null)
                {
                    foreach (var team in query)
                        teams.Add(team);
                }
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

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
            throw new NotImplementedException();
        }

        public Team GetTeamByName(string name)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void ModifyTeamByName(Team newTeam)
        {
            throw new NotImplementedException();
        }
    }
}

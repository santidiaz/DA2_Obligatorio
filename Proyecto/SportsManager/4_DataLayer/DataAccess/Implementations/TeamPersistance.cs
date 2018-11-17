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
        public void AddTeam(Team newTeam, int Id)
        {
            using (Context context = new Context())
            {
                Sport sportInDB = context.Sports.OfType<Sport>()
                    .Include(t => t.Teams)
                    .Where(s => s.Id.Equals(Id)).FirstOrDefault();

                context.Teams.Add(newTeam);
                sportInDB.Teams.Add(newTeam);
                context.SaveChanges();
            }
        }

        public void ModifyTeam(Team teamToModify)
        {
            using (Context context = new Context())
            {
                Team teamOnDB = context.Teams.OfType<Team>()
                    .Where(t => t.Id.Equals(teamToModify.Id))
                    .FirstOrDefault();

                teamOnDB.Name = string.IsNullOrEmpty(teamToModify.Name) ? teamOnDB.Name : teamToModify.Name;
                teamOnDB.Photo = teamToModify.Photo;
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

        public List<Team> GetTeams(string teamName, bool orderAsc)
        {
            var teams = new List<Team>();
            using (Context context = new Context())
            {
                //Ordeno ascendente y por numbre de equipo.
                if (orderAsc && !string.IsNullOrEmpty(teamName))
                {
                    teams = context.Teams.OfType<Team>().Where(t => t.Name.Contains(teamName)).OrderBy(o => o.Name).ToList();
                }
                //Ordeno descendente y por numbre de equipo.
                else if (!orderAsc && !string.IsNullOrEmpty(teamName))
                {
                    teams = context.Teams.OfType<Team>().Where(t => t.Name.Contains(teamName)).OrderByDescending(o => o.Name).ToList();
                }
                //Ordeno solo ascendente.
                else if (orderAsc)
                {
                    teams = context.Teams.OfType<Team>().OrderBy(o => o.Name).ToList();
                }
                //Ordeno solo descendente.
                else
                {
                    teams = context.Teams.OfType<Team>().OrderByDescending(o => o.Name).ToList();
                }
            }
            return teams;
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

        public bool IsTeamInSystem(Team team)
        {
            bool result = false;
            using (Context context = new Context())
            {
                result = context.Teams.OfType<Team>()
                    .Where(t => t.Name.Equals(team.Name))
                    .FirstOrDefault() != null;
            }
            return result;
        }

        public List<Event> GetEventsByTeam(Team team)
        {
            List<Event> teamEvents;
            using (Context context = new Context())
            {
                teamEvents = context.Events.OfType<Event>()
                    .Include(s => s.Sport)
                    .Include(t => t.EventTeams)
                    .Where(e => e.EventTeams.Exists(ev_tm => ev_tm.TeamId.Equals(team.Id)))
                    .ToList();
            }
            return teamEvents;
        }

        public Team GetTeamById(int teamId)
        {
            Team foundTeam;
            using (Context context = new Context())
            {
                foundTeam = context.Teams.OfType<Team>()
                    .FirstOrDefault(u => u.Id.Equals(teamId));
            }
            return foundTeam;
        }

        public bool ValidateTeamOnEvents(Team team)
        {
            bool result = false;
            using (Context context = new Context())
            {
                Event teamOnDB1 = context.Events.OfType<Event>().Include(a => a.EventTeams).Where(e => e.EventTeams.Exists(ev_tm => ev_tm.TeamId.Equals(team.Id))).FirstOrDefault();

                if (teamOnDB1 != null) result = true;
            }
            return result;
        }
    }
}
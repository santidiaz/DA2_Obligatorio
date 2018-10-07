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
        public void AddTeam(Team newTeam, int sportOID)
        {
            using (Context context = new Context())
            {
                var sportOnDB = context.Sports.Where(u => u.SportOID.Equals(sportOID)).FirstOrDefault();
                sportOnDB.Teams.Add(newTeam);
                //context.Teams.Add(newTeam);
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

        public List<Team> GetTeams(bool asc, string teamName)
        {
            var teams = new List<Team>();
            using (Context context = new Context())
            {
                //Ordeno ascendente y por numbre de equipo.
                if (asc && !string.IsNullOrEmpty(teamName))
                {
                    teams = context.Teams.OfType<Team>().Where(t => t.Name.Contains(teamName)).OrderBy(o => o.Name).ToList();
                }
                //Ordeno descendente y por numbre de equipo.
                else if (!asc && !string.IsNullOrEmpty(teamName))
                {
                    teams = context.Teams.OfType<Team>().Where(t => t.Name.Contains(teamName)).OrderByDescending(o => o.Name).ToList();
                }
                //Ordeno solo ascendente.
                else if (asc)
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

        public void ModifyTeam(Team teamToModify)
        {
            using (Context context = new Context())
            {
                var teamOnDB = context.Teams.OfType<Team>().Where(a => a.TeamOID.Equals(teamToModify.TeamOID)).FirstOrDefault();

                teamOnDB.Name = teamToModify.Name;
                teamOnDB.Photo = teamToModify.Photo;

                context.SaveChanges();
            }
        }

        public List<Event> GetEventsByTeam(Team team)
        {
            List<Event> result = new List<Event>();
            using (Context context = new Context())
            {
                List<Event> sportOnDB1 = context.Events.OfType<Event>().Include(s => s.Sport).Include(t => t.Away).Include(t => t.Local).ToList();

                if (sportOnDB1 != null && sportOnDB1.Count > 0)
                {
                    result.AddRange(sportOnDB1.Where(s => s.Away.TeamOID == team.TeamOID));
                    result.AddRange(sportOnDB1.Where(s => s.Local.TeamOID == team.TeamOID));
                }
            }
            return result;
        }

        public Team GetTeamByOID(int oid)
        {
            Team foundTeam;
            using (Context context = new Context())
            {
                foundTeam = context.Teams.OfType<Team>().FirstOrDefault(u => u.TeamOID.Equals(oid));
            }
            return foundTeam;
        }

        public bool ValidateTeamOnEvents(Team team)
        {
            bool result = false;
            using (Context context = new Context())
            {
                Event teamOnDB1 = context.Events.OfType<Event>().Where(a => a.GetLocalTeam().Equals(team.TeamOID)).FirstOrDefault();
                Event teamOnDB2 = context.Events.OfType<Event>().Where(a => a.GetAwayTeam().Equals(team.TeamOID)).FirstOrDefault();

                if (teamOnDB1 != null) result = true;
                if (teamOnDB2 != null) result = true;
            }
            return result;
        }


    }
}
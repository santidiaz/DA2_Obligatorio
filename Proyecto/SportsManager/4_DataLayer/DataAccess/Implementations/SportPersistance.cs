using BusinessEntities;
using BusinessEntities.JoinEntities;
using DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class SportPersistance : ISportPersistance
    {
        public void AddSport(Sport newSport)
        {
            using (Context context = new Context())
            {
                context.Sports.Add(newSport);
                context.SaveChanges();
            }
        }

        public void DeleteSport(Sport sportToDelete)
        {
            using (Context context = new Context())
            {
                context.Sports.Attach(sportToDelete);
                context.Sports.Remove(sportToDelete);
                context.SaveChanges();
            }
        }

        public void ModifySport(Sport sportToModify)
        {
            using (Context context = new Context())
            {
                Sport sportOnDB = context.Sports.OfType<Sport>()
                    .Where(a => a.Id.Equals(sportToModify.Id))
                    .FirstOrDefault();

                sportOnDB.Name = sportToModify.Name;
                context.SaveChanges();
            }
        }

        public List<Sport> GetSports()
        {
            List<Sport> sports;
            using (Context context = new Context())
            {
                sports = context.Sports.OfType<Sport>().Include(s => s.Teams).ToList();
            }
            return sports;
        }

        public Sport GetSportByName(string name, bool eageLoad = false)
        {
            Sport foundSport;
            using (Context context = new Context())
            {
                if (eageLoad)
                {
                    foundSport = context.Sports.OfType<Sport>()
                           .Include(s => s.Teams)
                           .FirstOrDefault(u => u.Name.Equals(name));
                }
                else
                {
                    foundSport = context.Sports.OfType<Sport>()
                        .FirstOrDefault(u => u.Name.Equals(name));
                }   
            }
            return foundSport;
        }

        public Sport GetSportById(int id, bool eagerLoad = false)
        {
            Sport foundSport;
            using (Context context = new Context())
            {
                if (eagerLoad)
                {
                    foundSport = context.Sports.OfType<Sport>()
                        .Include(s => s.Teams)
                        .FirstOrDefault(s => s.Id.Equals(id));
                }
                else
                {
                    foundSport = context.Sports.OfType<Sport>()
                        .FirstOrDefault(s => s.Id.Equals(id));
                }
            }
            return foundSport;
        }

        public bool IsSportInSystem(Sport sport)
        {
            bool result;
            using (Context context = new Context())
            {
                result = context.Sports.OfType<Sport>()
                    .Where(s => s.Name.Equals(sport.Name))
                    .FirstOrDefault() != null;
            }
            return result;
        }
        
        public List<Event> GetEventsBySport(Sport sport)
        {
            List<Event> sportEvents;
            using (Context context = new Context())
            {
                sportEvents = context.Events.OfType<Event>()
                    .Include(s => s.Sport)
                    .Include(t => t.EventTeams)
                    .Include(r => r.Result).ThenInclude(r => r.TeamsResult)
                    .Where(e => e.Sport.Name.Equals(sport.Name))
                    .ToList();

                // Add event teams.
                sportEvents.ForEach(e =>
                {
                    e.EventTeams = context.EventTeams.OfType<EventTeam>()
                        .Include(et => et.Team)
                        .Where(et => et.EventId.Equals(e.Id))
                        .ToList();
                });                
            }

            return sportEvents;
        }

        public bool CanBeDeleted(Sport sport)
        {
            bool result;
            using (Context context = new Context())
            {
                Sport sports = context.Sports.OfType<Sport>()
                    .Include(t => t.Teams)
                    .Where(s => s.Name.Equals(sport.Name))
                    .FirstOrDefault();

                result = sport.Teams == null || sport.Teams.Count.Equals(0);
            }
            return result;
        }
    }
}

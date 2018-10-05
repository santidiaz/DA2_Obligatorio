using BusinessEntities;
using DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;

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

        public void DeleteSportByName(Sport sportToDelete)
        {
            using (Context context = new Context())
            {
                context.Sports.Attach(sportToDelete);
                context.Sports.Remove(sportToDelete);
                context.SaveChanges();
            }
        }

        

        public List<Sport> GetSports()
        {
            var sports = new List<Sport>();
            using (Context context = new Context())
            {
                sports = context.Sports.OfType<Sport>().ToList();
            }
            return sports;
        }

        public bool IsSportInSystem(Sport sport)
        {
            bool result = false;
            using (Context context = new Context())
            {
                var sportOnDB = context.Sports.OfType<Sport>().Where(a => a.SportOID.Equals(sport.SportOID)).FirstOrDefault();

                result = sportOnDB != null ? true : false;
            }
            return result;
        }

        public void ModifySportByName(Sport sportToModify)
        {
            using (Context context = new Context())
            {
                var sportOnDB = context.Sports.OfType<Sport>().Where(a => a.SportOID.Equals(sportToModify.SportOID)).FirstOrDefault();
                sportOnDB.Name = sportToModify.Name;

                context.SaveChanges();
            }
        }

        public List<Event> GetEventsBySport(Sport sport)
        {
            List<Event> result = new List<Event>();
            using (Context context = new Context())
            {
                List<Event> sportOnDB1 = context.Events.OfType<Event>().Where(a => a.Sport.Equals(sport.SportOID)).ToList();

                if (sportOnDB1 != null) result.AddRange(sportOnDB1);
            }
            return result;
        }

        public Sport GetSportByName(string name, bool eageLoad)
        {
            Sport foundSport;
            using (Context context = new Context())
            {
                if(eageLoad)
                    foundSport = context.Sports.OfType<Sport>().Include(s => s.Teams).FirstOrDefault(u => u.Name.Equals(name));
                else
                    foundSport = context.Sports.OfType<Sport>().FirstOrDefault(u => u.Name.Equals(name));
            }
            return foundSport;
        }

        public bool ValidateSportOnTeams(Sport sport)
        {
            bool result = false;
            using (Context context = new Context())
            {
                //Team teamOnDB1 = context.Teams.OfType<Team>().Where(a => a.Sport.SportOID.Equals(sport.SportOID)).FirstOrDefault();
                Sport sports = context.Sports.OfType<Sport>().Where(a => a.SportOID.Equals(sport.SportOID)).FirstOrDefault();
                //Team team = sports.Find(s => s.Teams.Find(t => t.))
                if (sports.Teams != null && sports.Teams.Count > 0) result = true;
            }
            return result;
        }
    }
}

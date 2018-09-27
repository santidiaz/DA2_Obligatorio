using BusinessEntities;
using DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public void DeleteSportByName(string name)
        {
            using (Context context = new Context())
            {
                Sport sportToDelete = new Sport() { Name = name };
                context.Sports.Attach(sportToDelete);
                context.Sports.Remove(sportToDelete);
                context.SaveChanges();
            }
        }

        public Sport GetSportByName(string name)
        {
            Sport foundSport;
            using (Context context = new Context())
            {
                foundSport = context.Sports.OfType<Sport>().FirstOrDefault(u => u.Name.Equals(name));
            }
            return foundSport;
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
                var sportOnDB = context.Sports.OfType<Sport>().Include("Sports").Where(a => a.SportOID.Equals(sport.SportOID)).FirstOrDefault();

                result = sportOnDB != null ? true : false;
            }
            return result;
        }

        public void ModifySportByName(string name, Sport sportToModify)
        {
            using (Context context = new Context())
            {
                var sportOnDB = context.Sports.OfType<Sport>().Include("Sports").Where(a => a.SportOID.Equals(sportToModify.SportOID)).FirstOrDefault();

                sportOnDB.Name = sportToModify.Name;
                sportOnDB.TeamsList = sportToModify.TeamsList;

                context.SaveChanges();
            }
        }
    }
}

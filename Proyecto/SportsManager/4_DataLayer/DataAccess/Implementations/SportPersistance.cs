using BusinessEntities;
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
        /*
         public void AddUser(User newUser)
        {
            using (Context context = new Context())
            {
                context.users.Add(newUser);
                context.SaveChanges();
            }
        }
         */

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
    }
}

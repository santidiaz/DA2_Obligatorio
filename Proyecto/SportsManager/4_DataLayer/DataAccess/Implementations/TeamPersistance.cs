using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Implementations
{
    public class TeamPersistance : ITeamPersistance
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
        public void AddTeam(Team newTeam)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void ModifyTeamByName(Team newTeam)
        {
            throw new NotImplementedException();
        }
    }
}

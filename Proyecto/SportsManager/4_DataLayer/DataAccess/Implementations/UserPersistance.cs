using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Implementations
{
    public class UserPersistance : IUserPersistance
    {
        public bool DoesUserExists(string userName)
        {
            return true;
        }

        //public void AddUser(User newUser)
        //{
        //    using (Context context = new Context())
        //    {
        //        context.Users.Add(newUser);
        //        context.SaveChanges();
        //    }
        //}
    }
}

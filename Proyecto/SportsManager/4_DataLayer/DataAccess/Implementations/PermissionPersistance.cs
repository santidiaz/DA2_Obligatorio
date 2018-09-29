using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class PermissionPersistance : IPermissionPersistance
    {
        public bool HasPermission(Guid token, bool adminRequired)
        {
            using (Context context = new Context())
            {
                return context.Users.OfType<User>()
                    .Where(u => 
                        u.Token != null && 
                        u.Token.Equals(Guid.Empty) && 
                        u.Token.Equals(token) && 
                        u.IsAdmin.Equals(adminRequired)).Any();
            }
        }

        public void LogIn(string userName, Guid token)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.OfType<User>().Where(u => u.UserName.Equals(userName)).FirstOrDefault();
                userOnDB.Token = token;

                context.SaveChanges();
            }           
        }

        public void LogOut(Guid token)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.OfType<User>().Where(u => u.Token.Equals(token)).FirstOrDefault();
                userOnDB.Token = Guid.Empty;

                context.SaveChanges();
            }
        }
    }
}

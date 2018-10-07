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
                // Filter DB users with !null && !Emprty tokens on DB
                // Check if exists a User with the same token as the one given by parameter
                // If adminRequired check that the user found has [IsAdmin = true] otherwise return OK.
                return context.Users.OfType<User>()
                    .Where(u => 
                        u.Token != null && 
                        !u.Token.Equals(Guid.Empty) && 
                        u.Token.Equals(token) && 
                        (adminRequired ? u.IsAdmin.Equals(adminRequired) : true)).Any();
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

        public void LogOut(string userName)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.OfType<User>().Where(u => u.UserName.Equals(userName)).FirstOrDefault();
                if (userOnDB != null)
                {
                    userOnDB.Token = Guid.Empty;
                    context.SaveChanges();
                }
            }
        }
    }
}

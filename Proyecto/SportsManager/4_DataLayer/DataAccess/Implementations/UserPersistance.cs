using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Implementations
{
    public class UserPersistance : IUserPersistance
    {
        public bool DoesUserExists(string userName)
        {
            using (Context context = new Context())
            {
                return context.Users.OfType<User>().Any(u => u.UserName.Equals(userName));
            }
        }

        public void AddUser(User newUser)
        {
            using (Context context = new Context())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }

        public User GetUserByUserName(string userName)
        {
            User foundUser;
            using (Context context = new Context())
            {
                foundUser = context.Users.OfType<User>().FirstOrDefault(u => u.UserName.Equals(userName));
            }
            return foundUser;
        }

        public void DeleteUser(User userToDelete)
        {
            using (Context context = new Context())
            {
                context.Users.Attach(userToDelete);
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        public void ModifyUser(User userToModify)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.OfType<User>().Where(u => u.UserOID.Equals(userToModify.UserOID)).FirstOrDefault();

                userOnDB.Name = userToModify.Name;
                userOnDB.LastName = userToModify.LastName;
                userOnDB.Email = userToModify.Email;

                context.SaveChanges();
            }
        }

        public void AddFavoritesToUser(User user, List<Team> list)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.OfType<User>().Where(u => u.UserName.Equals(user.UserName)).FirstOrDefault();
                userOnDB.FavouriteTeams.AddRange(list);
                context.SaveChanges();
            }
            throw new NotImplementedException();
        }

        public List<Team> GetFavoritesTeamsByUserName(string userName)
        {
            throw new NotImplementedException();
        }
        
    }
}

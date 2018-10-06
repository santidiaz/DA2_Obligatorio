using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessEntities.JoinEntities;

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
                context.Users.Attach(newUser);
                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }

        public User GetUserByUserName(string userName, bool useEagerLoading = false)
        {
            User foundUser;
            using (Context context = new Context())
            {
                if (!useEagerLoading)
                    foundUser = context.Users.OfType<User>().FirstOrDefault(u => u.UserName.Equals(userName));
                else
                    foundUser = context.Users.Include(u => u.FavouriteTeams).Where(u => u.UserName.Equals(userName)).FirstOrDefault();
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

        public void ModifyUserFavouriteTeams(User userToModify, List<Team> newFavouriteTeams)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.Include(u => u.FavouriteTeams).Where(u => u.UserOID.Equals(userToModify.UserOID)).FirstOrDefault();

                userOnDB.FavouriteTeams = new List<UserTeam>();
                foreach (Team t in newFavouriteTeams)
                {
                    userOnDB.FavouriteTeams.Add(new UserTeam { TeamOID = t.TeamOID, UserOID = userOnDB.UserOID });
                }

                context.SaveChanges();
            }
        }

        /*
         private void UpdateTeachers(Context context, Subject subjectOnDB, List<Teacher> addedTeachers, List<Teacher> deletedTeachers)
        {
            deletedTeachers.ForEach(c => subjectOnDB.Teachers.Remove(c));
            foreach (Teacher t in addedTeachers)
            {
                if (context.Entry(t).State == EntityState.Detached)
                    context.people.Attach(t);

                subjectOnDB.Teachers.Add(t);
            }
        }
         */
		 
		 
        public void AddFavoritesToUser(User user, List<Team> list)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.Users.Include(u => u.FavouriteTeams).Where(u => u.UserOID.Equals(user.UserOID)).FirstOrDefault();

                userOnDB.FavouriteTeams = new List<UserTeam>();
                foreach (Team t in list)
                {
                    userOnDB.FavouriteTeams.Add(new UserTeam { TeamOID = t.TeamOID, UserOID = userOnDB.UserOID });
                }

                context.SaveChanges();
            }
        }

        public List<UserTeam> GetFavoritesTeamsByUserName(User user)
        {
            List<UserTeam> listTeams = new List<UserTeam>();
            using (Context context = new Context())
            {
                listTeams = context.UserTeams.Where(u => u.UserOID == user.UserOID).ToList();
            }
            return listTeams;
        }

        public void DeleteFavoriteTeamByUser(Team team, User user)
        {
            using (Context context = new Context())
            {
                var userOnDB = context.UserTeams.Where(u => u.UserOID == user.UserOID && u.TeamOID == team.TeamOID).FirstOrDefault();
                context.UserTeams.Remove(userOnDB);
                context.SaveChanges();
            }
        }
    }
}

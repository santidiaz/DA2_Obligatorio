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
                userOnDB.IsAdmin = userToModify.IsAdmin;
                userOnDB.Password = userToModify.Password;

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

        public List<UserTeam> GetUserFavouriteTeams(User user)
        {
            List<UserTeam> userFavouriteTeams;
            using (Context context = new Context())
            {
                userFavouriteTeams = context.UserTeams.Where(u => u.UserOID.Equals(user.UserOID)).ToList();
            }
            return userFavouriteTeams;
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

        public List<Event> GetUserFavouriteTeamsEvents(User user)
        {
            List<Event> userFavouriteTeamsEvents = new List<Event>();
            List<UserTeam> userFavouriteTeams;

            using (Context context = new Context())
            {
                // Get the user favourite teams.
                userFavouriteTeams = context.UserTeams.OfType<UserTeam>()
                    .Where(u => u.UserOID.Equals(user.UserOID))
                    .ToList();

                foreach (UserTeam favouriteTeam in userFavouriteTeams)
                {
                    // Return all events of users favourite teams.
                    List<Event> currentFavouriteTeamEvents = context.Events.OfType<Event>()
                        .Include(s => s.Sport)
                        .Include(t => t.Teams)
                        .Include(c => c.Comments)
                        .Where(e => e.Teams.Exists(ev_tm => ev_tm.TeamOID.Equals(favouriteTeam.TeamOID)))
                        .ToList();
                    
                    foreach(Event currentEvent in currentFavouriteTeamEvents)
                    {
                        // If return list [userFavouriteTeamsEvents] dont have the event added already.
                        if (!userFavouriteTeamsEvents.Exists(ft_ev => ft_ev.EventOID.Equals(currentEvent.EventOID)))
                        {
                            userFavouriteTeamsEvents.Add(currentEvent);
                        }
                    }
                }
            }

            return userFavouriteTeamsEvents;
        }
    }
}

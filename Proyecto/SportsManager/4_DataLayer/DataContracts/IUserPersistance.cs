using BusinessEntities;
using System;
using System.Collections.Generic;

namespace DataContracts
{
    public interface IUserPersistance
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);
        User GetUserByUserName(string userName);
        void DeleteUser(User userToDelete);
        void ModifyUser(User userToModify);
        void AddFavoritesToUser(User user, List<Team> list);
        List<Team> GetFavoritesTeamsByUserName(User userName);
        void DeleteFavoriteTeamByUser(Team team, User user);
    }
}
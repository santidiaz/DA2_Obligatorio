using BusinessEntities;
using BusinessEntities.JoinEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface IUserLogic
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);
        User GetUserByUserName(string userName);
        void DeleteUserByUserName(string userName);
        void ModifyUser(User userWithModifications);
        void ModifyUserFavouriteTeams(string userName, List<string> teamNames);
        void AddFavoritesToUser(User mockedOriginalUser, List<Team> teamLists);
        List<UserTeam> GetFavoritesTeamsByUserName(string userName);
        void DeleteFavoriteTeamByUser(int Id, string userName);
        List<Event> GetUserFavouriteTeamsEvents(string userName);
    }
}

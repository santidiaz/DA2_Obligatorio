﻿using BusinessEntities;
using BusinessEntities.JoinEntities;
using System;
using System.Collections.Generic;

namespace DataContracts
{
    public interface IUserPersistance
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);
        User GetUserByUserName(string userName, bool userEagerLoading = false);
        void DeleteUser(User userToDelete);
        void ModifyUser(User userToModify);
        void ModifyUserFavouriteTeams(User userToModify, List<Team> newFavouriteTeams);
		void AddFavoritesToUser(User user, List<Team> list);
        List<UserTeam> GetFavoritesTeamsByUserName(User userName);
        void DeleteFavoriteTeamByUser(Team team, User user);
        List<Event> GetCommentsOfUserFavouriteTemasEvents(User user);
        List<User> GetUsers();
    }
}
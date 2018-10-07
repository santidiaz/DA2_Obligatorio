using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessEntities.JoinEntities;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserPersistance userProvider;
        private ITeamPersistance teamProvider;

        public UserLogic(IUserPersistance userPersistanceProvider, ITeamPersistance teamPersistanceProvider)
        {
            this.userProvider = userPersistanceProvider;
            this.teamProvider = teamPersistanceProvider;
        }

        #region Public methods
        public bool DoesUserExists(string userName)
        {
            return this.userProvider.DoesUserExists(userName);
        }

        public void AddUser(User newUser)
        {
            if (this.DoesUserExists(newUser.UserName))
                throw new EntitiesException(Constants.UserError.USER_ALREDY_EXISTS, ExceptionStatusCode.Conflict);

            newUser.Password = HashTool.GenerateHash(newUser.Password);
            this.userProvider.AddUser(newUser);
        }

        public User GetUserByUserName(string userName)
        {
            return this.userProvider.GetUserByUserName(userName);
        }

        public void DeleteUserByUserName(string userName)
        {
            User userToDelete = this.GetUserByUserName(userName);
            if (userToDelete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.userProvider.DeleteUser(userToDelete);
        }

        public void ModifyUser(User userWithModifications)
        {
            User userToModify = this.GetUserByUserName(userWithModifications.UserName);
            if (userToModify == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            if (!this.CheckForModifications(userToModify, userWithModifications))
                throw new EntitiesException(Constants.UserError.NO_CHANGES, ExceptionStatusCode.NotModified);

            this.userProvider.ModifyUser(userToModify);
        }

        public void ModifyUserFavouriteTeams(string userName, List<string> teamNames)
        {
            User foundUser = this.userProvider.GetUserByUserName(userName, true);
            if (foundUser == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            List<Team> newFavouriteTeams = new List<Team>();
            Team foundTeam;
            foreach (string teamName in teamNames)
            {
                foundTeam = this.teamProvider.GetTeamByName(teamName);
                if (foundTeam == null)
                    throw new EntitiesException(string.Concat(Constants.TeamErrors.TEAM_NAME_NOT_FOUND, teamName), ExceptionStatusCode.NotFound);

                newFavouriteTeams.Add(foundTeam);
            }

            this.userProvider.ModifyUserFavouriteTeams(foundUser, newFavouriteTeams);
        }

        public void AddFavoritesToUser(User aUser, List<Team> teamLists)
        {
            User userToDelete = this.GetUserByUserName(aUser.UserName);
            if (userToDelete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.userProvider.AddFavoritesToUser(aUser, teamLists);
        }

        public List<UserTeam> GetFavoritesTeamsByUserName(string userName)
        {
            User user = this.GetUserByUserName(userName);
            if (user == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            return this.userProvider.GetFavoritesTeamsByUserName(user);
        }

        public void DeleteFavoriteTeamByUser(int teamOID, string userName)
        {
            User userComplete = this.GetUserByUserName(userName);
            if (userComplete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            Team foundTeam = this.teamProvider.GetTeamByOID(teamOID);
            if (foundTeam == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);

            this.userProvider.DeleteFavoriteTeamByUser(foundTeam, userComplete);
        }

        public List<Event> GetCommentsOfUserFavouriteTemasEvents(string userName)
        {
            User user = this.GetUserByUserName(userName);
            if (user == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            return this.userProvider.GetCommentsOfUserFavouriteTemasEvents(user);
        }
        #endregion

        #region Private Methods
        private bool CheckForModifications(User userToModify, User userWithModifications)
        {
            bool nameWasModified = this.ModifyName(userToModify, userWithModifications.Name);
            bool lastNameWasModified = this.ModifyLastName(userToModify, userWithModifications.LastName);
            bool emailWasModified = this.ModifyEmail(userToModify, userWithModifications.Email);
            bool passwordWasModified = this.ModifyPassword(userToModify, userWithModifications.Password);
            bool isAdminFlagWasModified = this.ModifyIsAdmin(userToModify, userWithModifications.IsAdmin);

            return (nameWasModified || lastNameWasModified || emailWasModified ||
                    passwordWasModified || isAdminFlagWasModified);
        }
        private bool ModifyName(User userToModify, string newName)
        {
            bool wasModifed = false;
            if (!string.IsNullOrEmpty(userToModify.Name)
                && !userToModify.Name.Equals(newName))
            {
                userToModify.Name = newName;
                wasModifed = true;
            }
            return wasModifed;
        }
        private bool ModifyLastName(User userToModify, string newLastName)
        {
            bool wasModifed = false;
            if (!string.IsNullOrEmpty(userToModify.LastName)
                && !userToModify.LastName.Equals(newLastName))
            {
                userToModify.LastName = newLastName;
                wasModifed = true;
            }
            return wasModifed;
        }
        private bool ModifyEmail(User userToModify, string newEmail)
        {
            bool wasModifed = false;
            if (!string.IsNullOrEmpty(userToModify.Email)
                && !userToModify.Email.Equals(newEmail))
            {
                userToModify.Email = newEmail;
                wasModifed = true;
            }
            return wasModifed;
        }
        private bool ModifyPassword(User userToModify, string newPassword)
        {
            bool wasModifed = false;
            if (this.CheckForPasswordChanges(userToModify.Password, ref newPassword))
            {
                userToModify.Password = newPassword;
                wasModifed = true;
            }
            return wasModifed;
        }
        private bool CheckForPasswordChanges(string userPasword, ref string newPassword)
        {
            newPassword = HashTool.GenerateHash(newPassword);
            return !string.IsNullOrEmpty(newPassword)
                && !userPasword.Equals(newPassword);
        }
        private bool ModifyIsAdmin(User userToModify, bool newAdminFlagValue)
        {
            bool adminFlagWasModified = false;
            if(newAdminFlagValue != userToModify.IsAdmin)
            {
                adminFlagWasModified = true;
                userToModify.IsAdmin = newAdminFlagValue;
            }
            return adminFlagWasModified;
        }
        #endregion
    }
}

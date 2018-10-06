using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

        public bool DoesUserExists(string userName)
        {
            return this.userProvider.DoesUserExists(userName);
        }

        public void AddUser(User newUser)
        {
            if (this.DoesUserExists(newUser.UserName))
                throw new EntitiesException(Constants.UserError.USER_ALREDY_EXISTS, ExceptionStatusCode.Conflict);

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
            if(foundUser == null)
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

        public void AddFavoritesToUser(User mockedOriginalUser, List<Team> teamLists)
        {
            User userToDelete = this.GetUserByUserName(mockedOriginalUser.UserName);
            if (userToDelete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.userProvider.AddFavoritesToUser(mockedOriginalUser, teamLists);
        }

        public void GetFavoritesTeamsByUserName(string userName)
        {
            User user = this.GetUserByUserName(userName);
            if (user == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.userProvider.GetFavoritesTeamsByUserName(user);
        }

        public void DeleteFavoriteTeamByUser(int teamOID, string user)
        {
            User userComplete = this.GetUserByUserName(user);
            if (userComplete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            Team teamComplete = this.teamProvider.GetTeamByOID(teamOID);
            if (teamComplete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.userProvider.DeleteFavoriteTeamByUser(teamComplete, userComplete);
        }

        #region Private Methods
        private bool CheckForModifications(User userToModify, User userWithModifications)
        {
            bool nameWasModified = this.ModifyName(userToModify, userWithModifications.Name);
            bool lastNameWasModified = this.ModifyLastName(userToModify, userWithModifications.LastName);
            bool emailWasModified = this.ModifyEmail(userToModify, userWithModifications.Email);
            bool passwordWasModified = this.ModifyPassword(userToModify, userWithModifications.Password);
            bool isAdminFlagWasModified = userWithModifications.IsAdmin != userToModify.IsAdmin;

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
            return !string.IsNullOrEmpty(newPassword)
                && !userPasword.Equals(this.GenerateHash(newPassword));
        }
        private string GenerateHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
        #endregion
    }
}

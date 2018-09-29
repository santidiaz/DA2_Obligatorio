using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserPersistance persistanceProvider;

        public UserLogic(IUserPersistance provider)
        {
            this.persistanceProvider = provider;
        }

        public bool DoesUserExists(string userName)
        {
            return this.persistanceProvider.DoesUserExists(userName);
        }

        public void AddUser(User newUser)
        {
            if (this.DoesUserExists(newUser.UserName))
                throw new Exception(Constants.UserError.USER_ALREDY_EXISTS);

            this.persistanceProvider.AddUser(newUser);
        }

        public User GetUserByUserName(string userName)
        {
            return this.persistanceProvider.GetUserByUserName(userName);
        }

        public bool DeleteUserByUserName(string userName)
        {
            try
            {
                bool result = true;
                User userToDelete = this.GetUserByUserName(userName);

                if (userToDelete != null)
                    this.persistanceProvider.DeleteUser(userToDelete);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Errors.UNEXPECTED, ex);
            }            
        }

        public bool ModifyUser(User userWithModifications)
        {
            try
            {
                bool changesWereMade = false;
                User userToModify = this.GetUserByUserName(userWithModifications.UserName);

                if (userToModify == null)
                    throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);               

                if (this.CheckForModifications(userToModify, userWithModifications))
                {
                    this.persistanceProvider.ModifyUser(userToModify);
                    changesWereMade = true;
                }                

                return changesWereMade;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Errors.UNEXPECTED, ex);
            }
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
                && !userPasword.Equals(HashTool.GenerateHash(newPassword));
        }
        #endregion
    }
}

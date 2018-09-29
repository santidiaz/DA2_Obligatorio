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
                throw new EntitiesException(Constants.UserError.USER_ALREDY_EXISTS, ExceptionStatusCode.Conflict);

            this.persistanceProvider.AddUser(newUser);
        }

        public User GetUserByUserName(string userName)
        {
            return this.persistanceProvider.GetUserByUserName(userName);
        }

        public void DeleteUserByUserName(string userName)
        {
            User userToDelete = this.GetUserByUserName(userName);
            if (userToDelete == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            this.persistanceProvider.DeleteUser(userToDelete);
        }

        public void ModifyUser(User userWithModifications)
        {
            User userToModify = this.GetUserByUserName(userWithModifications.UserName);
            if (userToModify == null)
                throw new EntitiesException(Constants.UserError.USER_NOT_FOUND, ExceptionStatusCode.NotFound);

            if (!this.CheckForModifications(userToModify, userWithModifications))
                throw new EntitiesException(Constants.UserError.NO_CHANGES, ExceptionStatusCode.NotModified);

            this.persistanceProvider.ModifyUser(userToModify);
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

﻿using BusinessContracts;
using BusinessEntities;
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
                throw new Exception(Constants.Errors.USER_ALREDY_EXISTS);

            this.persistanceProvider.AddUser(newUser);
        }

        // Si no encuentra el usuario retorna null.
        public User GetUserByUserName(string userName)
        {
            return this.persistanceProvider.GetUserByUserName(userName); ;
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

                bool nameWasModified = this.ModifyName(userToModify, userWithModifications.Name);
                bool lastNameWasModified = this.ModifyLastName(userToModify, userWithModifications.LastName);
                bool emailWasModified = this.ModifyEmail(userToModify, userWithModifications.Email);
                bool passwordWasModified = this.ModifyPassword(userToModify, userWithModifications.Password);
                bool isAdminFlagWasModified = userWithModifications.IsAdmin != userToModify.IsAdmin;

                if (nameWasModified || lastNameWasModified || emailWasModified ||
                    passwordWasModified || isAdminFlagWasModified)
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

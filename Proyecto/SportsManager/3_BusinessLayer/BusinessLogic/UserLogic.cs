using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
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

        public User GetUserByUserName(string userName)
        {
            User userFound = this.persistanceProvider.GetUserByUserName(userName);
            if (userFound == null)
                throw new Exception(Constants.Errors.USER_NOT_FOUND);

            return userFound;
        }

        public void DeleteUser(string userName)
        {
            User userToDelete = this.GetUserByUserName(userName);
            this.persistanceProvider.DeleteUser(userToDelete);
        }
    }
}

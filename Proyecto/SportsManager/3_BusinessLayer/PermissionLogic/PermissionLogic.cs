using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using PermissionContracts;
using System;

namespace PermissionLogic
{
    public class PermissionLogic : IPermissionLogic
    {
        private readonly IPermissionPersistance permissionPersistance;
        private readonly IUserPersistance usersPersistance;

        public PermissionLogic(IPermissionPersistance permissionProvider, IUserPersistance usersProvider)
        {
            this.permissionPersistance = permissionProvider;
            this.usersPersistance = usersProvider;
        }

        #region Public methods
        public bool HasPermission(Guid token, bool isAdminRequired)
        {
            return this.permissionPersistance.HasPermission(token, isAdminRequired);
        }

        public Tuple<Guid, string, bool> LogIn(string userName, string password)
        {
            Guid newToken = Guid.Empty;
            User userToBeLogedIn = this.usersPersistance.GetUserByUserName(userName);
            if (userToBeLogedIn == null)
                throw new EntitiesException(Constants.PermissionError.USER_NOT_FOUND, ExceptionStatusCode.Unauthorized);

            bool isValid = this.IsUserPasswordValid(userToBeLogedIn.Password, password);
            if (!isValid)
                throw new EntitiesException(Constants.PermissionError.INVALID_PASSWORD, ExceptionStatusCode.Unauthorized);

            newToken = Guid.NewGuid();
            this.permissionPersistance.LogIn(userToBeLogedIn.UserName, newToken);

            return Tuple.Create(newToken, userToBeLogedIn.UserName, userToBeLogedIn.IsAdmin);
        }

        public void LogOut(string userName)
        {
            this.permissionPersistance.LogOut(userName);
        }
        #endregion

        #region Private methods
        private bool IsUserPasswordValid(string userPassword, string passwordToBeCompared)
        {
            bool result = false;

            string hashedPassword = HashTool.GenerateHash(passwordToBeCompared);
            if (userPassword.Equals(hashedPassword))
                result = true;

            return result;
        }
        #endregion
    }
}
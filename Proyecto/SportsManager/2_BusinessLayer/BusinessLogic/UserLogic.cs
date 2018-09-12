using BusinessContracts;
using DataContracts;
using System;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserPersistance PersistanceProvider;

        public UserLogic(IUserPersistance provider)
        {
            this.PersistanceProvider = provider;
        }
    }
}

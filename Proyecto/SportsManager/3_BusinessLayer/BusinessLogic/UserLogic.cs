using BusinessContracts;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

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

using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface IUserLogic
    {
        bool DoesUserExists(string userName);
        //void AddUser(User newUser);
    }
}

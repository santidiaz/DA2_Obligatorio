using BusinessEntities;
using System;

namespace DataContracts
{
    public interface IUserPersistance
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);
        //void ModifyUser(User userToModify);
        //void DeleteUser(string userName);
    }
}
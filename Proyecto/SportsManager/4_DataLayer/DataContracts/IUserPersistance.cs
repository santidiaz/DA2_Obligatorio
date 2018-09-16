using BusinessEntities;
using System;

namespace DataContracts
{
    public interface IUserPersistance
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);
        User GetUserByUserName(string userName);
        void DeleteUser(User userToDelete);
        //void ModifyUser(User userToModify);
    }
}
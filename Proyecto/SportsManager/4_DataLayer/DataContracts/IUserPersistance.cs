using BusinessEntities;
using System;

namespace DataContracts
{
    public interface IUserPersistance
    {
        bool DoesUserExists(string userName);
        void AddUser(User newUser);

        User GetUserByUserName(string userName);
        //void ModifyUser(User userToModify);
        //void DeleteUser(string userName);
    }
}
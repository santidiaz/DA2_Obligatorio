using System;

namespace PermissionContracts
{
    public interface IPermissionLogic
    {
        Tuple<Guid, bool> LogIn(string userName, string password);
        void LogOut(string userName);
        bool HasPermission(Guid token, bool isAdminRequired);
    }
}

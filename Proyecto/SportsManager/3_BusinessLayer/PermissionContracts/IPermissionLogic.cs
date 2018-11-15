using System;

namespace PermissionContracts
{
    public interface IPermissionLogic
    {
        Tuple<Guid, string, bool> LogIn(string userName, string password);
        void LogOut(string userName);
        bool HasPermission(Guid token, bool isAdminRequired);
    }
}

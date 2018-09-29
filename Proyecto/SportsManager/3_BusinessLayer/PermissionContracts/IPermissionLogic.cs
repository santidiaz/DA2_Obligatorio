using System;

namespace PermissionContracts
{
    public interface IPermissionLogic
    {
        Guid LogIn(string userName, string password);
        void LogOut(Guid token);
        bool HasPermission(Guid token, bool isAdminRequired);
    }
}

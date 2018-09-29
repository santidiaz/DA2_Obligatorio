using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface IPermissionPersistance
    {
        void LogIn(string userName, Guid token);
        void LogOut(Guid token);
        bool HasPermission(Guid token, bool adminRequired);
    }
}

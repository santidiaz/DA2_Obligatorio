using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Filters
{
    public class PermissionFilter : Attribute, IActionFilter
    {
        private readonly bool _adminRequired;
        private readonly IPermissionLogic permissions = ProviderManager.Provider.GetInstance.GetPermissionOperations();

        public PermissionFilter(bool adminRequried)
        {
            _adminRequired = adminRequried;            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Get token from header.
            string token = context.HttpContext.Request.Headers["Authorization"];

            // If token is not valid.
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult()
                {
                    Content = "Token is required",
                };
            }

            Guid requestGuid = new Guid(token);
            // VALIDAMOS EL TOKEN
            if (!this.permissions.HasPermission(requestGuid, _adminRequired))
            {
                context.Result = new ContentResult()
                {
                    Content = "Invalid Token",
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}

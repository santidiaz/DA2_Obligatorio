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
            // No action required
        }

        public void OnActionExecuting(ActionExecutingContext context)
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

            // Valido si tiene permisos el endpoint que se quiere consumir.
            if (context.Result == null && !this.permissions.HasPermission(new Guid(token), _adminRequired))
            {
                context.Result = new ContentResult()
                {
                    Content = "User don't have permission for this operation.",
                };
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Filters
{
    public class PermissionFilter : Attribute, IActionFilter
    {
        private readonly bool _adminRequired;
        //private readonly ISessionRepository sessions;

        public PermissionFilter(bool adminRequried)
        {
            _adminRequired = adminRequried;
            //this.sessions = sessions; // pendiente invocar context factory
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Get token from header.
            string token = context.HttpContext.Request.Headers["Authorization"];
            
            // If token is not valid.
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    Content = "Token is required",
                };
            }

            // VALIDAMOS EL TOKEN
            //if (!this.sessions.IsValidToken(token, _adminRequired))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        Content = "Invalid Token",
            //    };
            //}
            //// CHECKEAMOS QUE EL TOKEN TENGA LOS PERMISOS NECESARIOS
            //if (!sessions.HasLevel(token, _role))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        Content = "The user isen't " + _role,
            //    };
            //}
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}

using Logger;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace SportsWebApi.Filters
{
    public class Log : Attribute, IActionFilter
    {
        private readonly string _action;
        private readonly ILogger logger = LogProvider.Logger.GetInstance.LogTool();

        public Log(string action)
        {
            _action = action;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action required

            var algo = context.HttpContext.Request.Headers;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //// Get token from header.
            var algo = context.HttpContext.Request.Headers;

            //// If token is not valid.
            //if (string.IsNullOrEmpty(token))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        Content = "Token is required",
            //    };
            //}

            //// Valido si tiene permisos el endpoint que se quiere consumir.
            //if (context.Result == null && !this.permissions.HasPermission(new Guid(token), _adminRequired))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        Content = "User don't have permission for this operation.",
            //    };
            //}
        }
    }
}

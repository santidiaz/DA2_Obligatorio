﻿using BusinessEntities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Utilities
{
    public static class Utility
    {
        public static int GetStatusResponse(EntitiesException exception)
        {
            int response = 500;
            switch (exception.StatusCode)
            {
                case ExceptionStatusCode.NotModified:
                    response = 304;
                    break;
                case ExceptionStatusCode.NotFound:
                    response = 404;
                    break;
                case ExceptionStatusCode.InvalidData:
                    response = 400;
                    break;
                case ExceptionStatusCode.Conflict:
                    response = 409; // Conflict with the resource, for example trying to add an object that already exists in DB.
                    break;
                case ExceptionStatusCode.Undefined:
                    response = 500;
                    break;
                default:
                    break;
            }
            return response;
        }
    }
}

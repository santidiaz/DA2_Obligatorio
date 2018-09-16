using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtilities
{
    public static class Constants
    {
        public static class Errors
        {
            public const string UNEXPECTED = "Unexpected error.";

            #region User Errors
            public const string INVALID_EMAIL_FORMAT = "Invalid email format.";
            public const string NAME_REQUIRED = "Name is required.";
            public const string LAST_NAME_REQUIRED = "Last name is required.";
            public const string USER_NAME_REQUIRED = "User name is required.";
            public const string PASSWORD_REQUIRED = "Password is required.";
            public const string USER_ALREDY_EXISTS = "UserName alredy exists.";
            #endregion
        }
    }
}

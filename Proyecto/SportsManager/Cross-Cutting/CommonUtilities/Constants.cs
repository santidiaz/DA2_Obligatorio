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
            public const string USER_NOT_FOUND = "User not found.";
            public const string NO_MODIFICATIONS = "No modifications have been made.";
            #endregion
        }

        public static class Team
        {
            public const string NAME_TEST = "Nacional";
            public const string NAME_TEST_MODIFY = "Nacional 2";
            public const string NAME_TEST_PENIAROL = "Peniarol";
        }

        public static class TeamErrors
        {
            public const string ERROR_TEAM_ALREADY_EXISTS = "Team already exists.";
            public const string ERROR_TEAM_NOT_EXISTS = "Team not exists.";
            public const string NAME_REQUIRED = "Name required";
            public const string PHOTO_INVALID = "Invalid photo";
        }
    }
}

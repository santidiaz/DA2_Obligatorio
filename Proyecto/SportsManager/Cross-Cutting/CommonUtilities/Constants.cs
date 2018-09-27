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
            public const string NO_MODIFICATIONS = "No modifications have been made.";
        }

        public static class UserError
        {
            public const string INVALID_EMAIL_FORMAT = "Invalid email format.";
            public const string NAME_REQUIRED = "Name is required.";
            public const string LAST_NAME_REQUIRED = "Last name is required.";
            public const string USER_NAME_REQUIRED = "User name is required.";
            public const string PASSWORD_REQUIRED = "Password is required.";
            public const string USER_ALREDY_EXISTS = "UserName alredy exists.";
            public const string USER_NOT_FOUND = "User not found.";
        }

        public static class EventError
        {
            public const string INVALID_DATE = "Date must be equal or greather than today.";
            public const string SPORT_REQUIRED = "Must specify a sport.";
            public const string ALREADY_EXISTS = "An event for the selected teams already exists today.";
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

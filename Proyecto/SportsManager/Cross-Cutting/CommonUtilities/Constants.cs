using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtilities
{
    public static class Constants
    {
        public static class Errors
        {
            public const string ERROR_UNEXPECTED = "Unexpected error.";
            public const string ERROR_INVALID_EMAIL_FORMAT = "Invalid email format.";
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

        public static class Sport
        {
            public const string NAME_SPORT_FUTBOL = "Futbol";
            public const string NAME_SPORT_TENIS = "Futbol";
        }

        public static class SportErrors
        {
            public const string NAME_REQUIRED = "Name required";
            public const string TEAMLIST_REQUIRED = "Team list required";
        }
    }
}

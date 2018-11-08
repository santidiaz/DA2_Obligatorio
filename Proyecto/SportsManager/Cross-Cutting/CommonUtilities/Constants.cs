using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
            public const string NO_CHANGES = "No changes were made.";
        }

        public static class EventError
        {
            public const string INVALID_DATE = "Date must be equal or greather than today.";
            public const string SPORT_REQUIRED = "Must specify a sport.";
            public const string ALREADY_EXISTS = "An event with selected teams already exists for the date.";
            public const string NOT_FOUND = "Event was not found.";
            public const string INVALID_AMOUNT_OF_TEAMS = "Invalid amount of teams.";
            public const string EVENT_TEAM_EXISTS = "The team [{0}] is already on an event on date {1}";
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
            public const string TEAM_NAME_NOT_FOUND = "The team <{0}> was not found";
            public const string TEAM_Id_FAIL = "The sport to add are invalid";
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
            public const string ERROR_SPORT_ALREADY_EXISTS = "Sport already exists";
            public const string ERROR_SPORT_DO_NOT_EXISTS = "Sport does not exists";
            public const string NOT_ENOUGH_TEAMS = "Not enough teams in the sport";
            public const string TEAM_NOT_IN_SPORT = "Team <{0}> does no belong to sport";
            public const string ERROR_SPORT_HAS_TEAMS = "Can't delete sport with teams";
            public const string NOT_ALL_TEAMS_BELONG_TO_SPORT = "Not all teams belong to the sport";
            public const string NO_TEAM_BELONG_TO_SPORT = "None of the teams belong to the sport";
            public const string REPEATED_TEAMS = "There are repeated teams";
        }

        public static class PermissionError
        {
            public const string USER_NOT_FOUND = "User not found.";
            public const string INVALID_PASSWORD = "Invalid password.";
        }
		
		public static class CommentError
        {
            public const string DESCRIPTION_REQUIRED = "Description required";
            public const string CREATORNAME_REQUIRED = "CreatorName required";
            public const string ERROR_CREATOR_NAME_NOT_EXISTS = "CreatorName not exists";
        }

        public static class Comment
        {
            public const string DESCRIPTION_TEST = "Description example";
            public const string CREATORNAME_TEST = "Santiago";
        }
    }
}
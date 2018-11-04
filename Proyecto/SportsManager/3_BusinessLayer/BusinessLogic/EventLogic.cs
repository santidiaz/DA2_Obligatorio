using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class EventLogic : IEventLogic
    {
        private IEventPersistance eventProvider;
        private ISportPersistance sportProvider;
        private ITeamPersistance teamProvider;

        public EventLogic(IEventPersistance eventPersistance, ISportPersistance sportPersistance, ITeamPersistance teamPersistance)
        {
            this.eventProvider = eventPersistance;
            this.sportProvider = sportPersistance;
            this.teamProvider = teamPersistance;
        }

        #region Public methods
        public void AddEvent(string sportName, List<string> teamNames, DateTime eventDate)
        {
            if(teamNames == null || teamNames.Count < 2)
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            Sport foundSport = this.FindSport(sportName);
            List<Team> foundTeams = this.FindSportTeams(foundSport, teamNames);
            this.ValidateTeamsEventExists(foundTeams, eventDate);

            Event newEvent = new Event(eventDate, foundSport, foundTeams);
            this.eventProvider.AddEvent(newEvent);
        }

        public void DeleteEventById(int eventId)
        {
            try
            {
                Event eventToBeDeleted = this.GetEventById(eventId);
                if (eventToBeDeleted == null)
                    throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);

                this.eventProvider.DeleteEvent(eventToBeDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Errors.UNEXPECTED, ex);
            }
        }

        public void ModifyEvent(int eventId, List<string> teamsNames, DateTime newDate)
        {
            Event eventToModify = this.GetEventById(eventId, true);
            List<Team> foundTeams = this.FindSportTeams(eventToModify.Sport, teamsNames);
            ValidateTeamsEventExists(foundTeams, newDate);

            eventToModify.ModifyTeams(foundTeams);
            eventToModify.InitialDate = newDate;

            this.eventProvider.ModifyEvent(eventToModify);
        }

        public List<Team> FindSportTeams(Sport foundSport, List<string> teamNames)
        {
            List<Team> sportTeams = foundSport.Teams;
            // Get all sport teams that are included in teamNames list
            List<Team> foundTeams = sportTeams.Where(st => !teamNames.Any(tn => st.Name.Equals(tn))).ToList();

            if (foundTeams == null)
                throw new EntitiesException(Constants.SportErrors.NO_TEAM_BELONG_TO_SPORT, ExceptionStatusCode.NotFound);
            else if (foundTeams.Count != teamNames.Count)
                throw new EntitiesException(Constants.SportErrors.NOT_ALL_TEAMS_BELONG_TO_SPORT, ExceptionStatusCode.InvalidData);

            return foundTeams;
        }

        public Event GetEventById(int eventId, bool eagerLoad = true)
        {
            Event foundEvent = this.eventProvider.GetEventById(eventId, eagerLoad);
            if (foundEvent == null)
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);

            return foundEvent;
        }

        public List<Event> GetAllEvents()
        {
            return this.eventProvider.GetAllEvents();
        }
        #endregion

        #region Private methods
        private void ValidateTeamsEventExists(List<Team> teams, DateTime eventDate)
        {
            List<Event> events = this.eventProvider.GetEventsByDate(eventDate);
            foreach(Event e in events)
            {
                e.Teams.ForEach(t => {
                    if(teams.Exists(tm => tm.Name.Equals(t)))
                    {
                        throw new EntitiesException(
                            string.Format(Constants.EventError.EVENT_TEAM_EXISTS,t.Name, e.InitialDate.Date), 
                            ExceptionStatusCode.InvalidData);
                    }
                });
            }
        }

        private Sport FindSport(string sportName)
        {
            Sport foundSport = this.sportProvider.GetSportByName(sportName, true);
            if (foundSport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return foundSport;
        }
        #endregion
    }
}
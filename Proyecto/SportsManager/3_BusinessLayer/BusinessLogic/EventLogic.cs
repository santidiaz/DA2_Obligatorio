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
        private readonly IEventPersistance eventProvider;
        private readonly ISportPersistance sportProvider;
        private readonly ITeamPersistance teamProvider;

        public EventLogic(IEventPersistance eventPersistance, ISportPersistance sportPersistance, ITeamPersistance teamPersistance)
        {
            this.eventProvider = eventPersistance;
            this.sportProvider = sportPersistance;
            this.teamProvider = teamPersistance;
        }

        #region Public methods
        public void AddEvent(string sportName, List<string> teamNames, DateTime eventDate)
        {
            var foundSport = this.FindSport(sportName);

            if (teamNames == null || teamNames.Count < 2)
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            if (this.CheckForDuplicatedNames(teamNames))
                throw new EntitiesException(Constants.SportErrors.REPEATED_TEAMS, ExceptionStatusCode.InvalidData);
            
            var foundTeams = this.FindTeams(teamNames);
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

        public void ModifyEvent(int eventId, List<string> teamNames, DateTime newDate)
        {
            if (this.CheckForDuplicatedNames(teamNames))
                throw new EntitiesException(Constants.SportErrors.REPEATED_TEAMS, ExceptionStatusCode.InvalidData);

            Event eventToModify = this.GetEventById(eventId, true);
            List<Team> foundTeams = this.FindTeams(teamNames);
            ValidateTeamsEventExistsWithEventId(foundTeams, newDate, eventId);

            eventToModify.ModifyTeams(foundTeams);
            eventToModify.InitialDate = newDate;

            this.eventProvider.ModifyEvent(eventToModify);
        }

        public List<Team> FindTeams(List<string> teamNames)
        {
            List<Team> temas = new List<Team>();

            foreach (var item in teamNames)
            {
                temas.Add(this.teamProvider.GetTeamByName(item));
            }

            return temas;
        }

        public Event GetEventById(int eventId, bool eagerLoad = true)
        {
            var foundEvent = this.eventProvider.GetEventById(eventId, eagerLoad);
            if (foundEvent == null)
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);

            return foundEvent;
        }

        public List<Event> GetAllEvents()
        {
            return this.eventProvider.GetAllEvents();
        }

        public void SetupEventResult(int eventId, List<string> teams, bool drawMatch = false)
        {
            var foundEvent = this.GetEventById(eventId, true);
            if (foundEvent.HasResult())
                throw new EntitiesException(Constants.EventError.EVENT_ALREADY_HAVE_RESULT, ExceptionStatusCode.InvalidData);

            bool multipleTeamsEvent = foundEvent.Sport.AllowdMultipleTeamsEvents;
            var eventResult = new EventResult(teams, multipleTeamsEvent, drawMatch);
            foundEvent.Result = eventResult;

            this.eventProvider.SaveEventResult(foundEvent);
        }

        #endregion

        #region Private methods
        private void ValidateTeamsEventExists(List<Team> teams, DateTime eventDate)
        {
            List<Event> events = this.eventProvider.GetEventsByDate(eventDate);
            foreach(Event e in events)
            {
                e.EventTeams.ForEach(t => {
                    if(teams.Exists(tm => tm.Id.Equals(t.TeamId)))
                    {
                        throw new EntitiesException(
                            string.Format(Constants.EventError.EVENT_TEAM_EXISTS,t.TeamId, e.InitialDate.Date), 
                            ExceptionStatusCode.InvalidData);
                    }
                });
            }
        }
        
         private void ValidateTeamsEventExistsWithEventId(List<Team> teams, DateTime eventDate, int eventId)
        {
            List<Event> events = this.eventProvider.GetEventsByDate(eventDate);
            foreach (Event e in events)
            {
                e.EventTeams.ForEach(t => {
                    if (teams.Exists(tm => tm.Id.Equals(t.TeamId)) && e.Id != eventId)
                    {
                        throw new EntitiesException(
                            string.Format(Constants.EventError.EVENT_TEAM_EXISTS, t.TeamId, e.InitialDate.Date),
                            ExceptionStatusCode.InvalidData);
                    }
                });
            }
        }

        private Sport FindSport(string sportName)
        {
            var foundSport = this.sportProvider.GetSportByName(sportName, true);
            if (foundSport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return foundSport;
        }

        private Sport FindSportById(int sportId)
        {
            Sport foundSport = this.sportProvider.GetSportById(sportId, true);
            if (foundSport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return foundSport;
        }

        private bool CheckForDuplicatedNames(List<string> names)
        {
            return !names.Count.Equals(names.Distinct().Count());
        }
        #endregion
    }
}
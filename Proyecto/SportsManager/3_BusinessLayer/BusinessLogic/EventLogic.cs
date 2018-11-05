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
            var foundSport = this.FindSport(sportName);

            if (teamNames == null || teamNames.Count < 2)
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            if (this.CheckForDuplicatedNames(teamNames))
                throw new EntitiesException(Constants.SportErrors.REPEATED_TEAMS, ExceptionStatusCode.InvalidData);
            
            var foundTeams = this.FindSportTeams(foundSport, teamNames);
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
            List<Team> foundTeams = this.FindSportTeams(eventToModify.Sport, teamNames);
            ValidateTeamsEventExists(foundTeams, newDate);

            eventToModify.ModifyTeams(foundTeams);
            eventToModify.InitialDate = newDate;

            this.eventProvider.ModifyEvent(eventToModify);
        }

        public List<Team> FindSportTeams(Sport foundSport, List<string> teamNames)
        {
            List<Team> sportTeams = foundSport.Teams;
            // Get all sport teams that are included in teamNames list
            List<Team> foundTeams = sportTeams.Where(st => teamNames.Any(tn => st.Name == tn)).ToList();

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
                e.EventTeams.ForEach(t => {
                    if(teams.Exists(tm => tm.TeamOID.Equals(t.TeamOID)))
                    {
                        throw new EntitiesException(
                            string.Format(Constants.EventError.EVENT_TEAM_EXISTS,t.TeamOID, e.InitialDate.Date), 
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

        private bool CheckForDuplicatedNames(List<string> names)
        {
            return !names.Count.Equals(names.Distinct().Count());
        }
        #endregion
    }
}
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void AddEvent(string sportName, string firstTeamName, string secondTeamName, DateTime eventDate)
        {
            Sport foundSport = this.FindSport(sportName);
            Team foundTeamA = this.FindTeamOnSport(foundSport, firstTeamName);
            Team foundTeamB = this.FindTeamOnSport(foundSport, secondTeamName);

            if (this.DoesTeamsEventExists(foundTeamA, foundTeamB, eventDate))
                throw new EntitiesException(Constants.EventError.ALREADY_EXISTS, ExceptionStatusCode.InvalidData);

            Event newEvent = new Event(eventDate, foundSport, foundTeamA, foundTeamB);
            this.eventProvider.AddEvent(newEvent);
        }

        public Event GetEventById(int eventId)
        {
            Event foundEvent = this.eventProvider.GetEventById(eventId, true);
            if (foundEvent == null)
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);

            return foundEvent;
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

        public void ModifyEvent(int eventId, string localTeamName, string awayTeamName, DateTime initialDate)
        {
            Event eventToModify = this.GetEventById(eventId);
            Team foundTeamA = this.FindTeamOnSport(eventToModify.Sport, localTeamName);
            Team foundTeamB = this.FindTeamOnSport(eventToModify.Sport, awayTeamName);
            
            if (this.DoesTeamsEventExists(foundTeamA, foundTeamB, initialDate))
                throw new EntitiesException(Constants.EventError.ALREADY_EXISTS, ExceptionStatusCode.InvalidData);

            eventToModify.Local = foundTeamA;
            eventToModify.Away = foundTeamB;
            eventToModify.InitialDate = initialDate;
            this.eventProvider.ModifyEvent(eventToModify);
        }

        #region Private methods
        private bool DoesTeamsEventExists(Team firstTeam, Team secondTeam, DateTime eventDate)
        {
            List<Event> events = this.eventProvider.GetEventsByDate(eventDate);
            return events?.Exists(te => te.GetLocalTeam().Equals(firstTeam)
                                         && te.GetAwayTeam().Equals(secondTeam)) ?? false;
        }

        private Sport FindSport(string sportName)
        {
            Sport foundSport = this.sportProvider.GetSportByName(sportName, true);
            if (foundSport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return foundSport;
        }

        private Team FindTeamOnSport(Sport aSport, string teamName)
        {
            Team foundTeam = aSport.Teams.Find(t => t.Name.Equals(teamName));
            if (foundTeam == null)
                throw new EntitiesException(string.Format(Constants.SportErrors.TEAM_NOT_IN_SPORT, teamName), ExceptionStatusCode.NotFound);

            return foundTeam;
        }

        public List<Event> GetAllEvents()
        {
            return this.eventProvider.GetAllEvents();
        }
        #endregion
    }
}
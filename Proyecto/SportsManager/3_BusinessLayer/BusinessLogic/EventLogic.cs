using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class EventLogic : IEventLogic
    {
        private IEventPersistance PersistanceProvider;

        public EventLogic(IEventPersistance provider)
        {
            this.PersistanceProvider = provider;
        }

        public void AddEvent(Event newEvent)
        {
            if (this.DoesTeamsEventExists(newEvent))
                throw new EntitiesException(Constants.EventError.ALREADY_EXISTS, ExceptionStatusCode.InvalidData);

            this.PersistanceProvider.AddEvent(newEvent);
        }

        public List<Event> GenerateFixture(IFixture fixtureGenerator)
        {




            return null;
            //return fixtureGenerator.GenerateFixture(anEvent.Sport);
        }

        #region Private methods
        private bool DoesTeamsEventExists(Event eventToValidate)
        {
            List<Event> todayEvents = this.PersistanceProvider.GetTodayEvents();
            return todayEvents.Exists(te => te.GetFirstTeam().Equals(eventToValidate.GetFirstTeam())
                                         && te.GetSecondTeam().Equals(eventToValidate.GetSecondTeam()));
        }
        #endregion
    }
}
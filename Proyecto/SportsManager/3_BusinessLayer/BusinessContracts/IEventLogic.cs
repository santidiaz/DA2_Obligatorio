using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface IEventLogic
    {
        void AddEvent(string sportName, string firstTeamName, string secondTeamName, DateTime eventDate);
        void DeleteEventById(int eventId);
        Event GetEventById(int eventId);
        List<Event> GetAllEvents();
        List<Event> GenerateFixture(IFixture fixtureGenerator);
    }
}

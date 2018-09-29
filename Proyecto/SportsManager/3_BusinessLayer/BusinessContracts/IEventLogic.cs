using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface IEventLogic
    {
        void AddEvent(Event newEvent);
        void DeleteEventById(int eventId);
        Event GetEventById(int eventId);
        List<Event> GenerateFixture(IFixture fixtureGenerator);
    }
}

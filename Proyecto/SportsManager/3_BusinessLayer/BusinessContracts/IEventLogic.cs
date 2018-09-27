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
        List<Event> GenerateFixture(IFixture fixtureGenerator);
    }
}

using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface IEventPersistance
    {
        void AddEvent(Event newEvent);
        List<Event> GetTodayEvents();
        List<Event> GetAllEvents();
    }
}

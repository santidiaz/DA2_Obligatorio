using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface IEventPersistance
    {
        void AddEvent(Event newEvent);
        void DeleteEvent(Event eventToBeDeleted);
        Event GetEventById(int eventId);
        List<Event> GetTodayEvents();
        List<Event> GetAllEvents();
        List<Event> GetEventsByDate(DateTime eventDate);
    }
}

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
        void ModifyEvent(Event eventToModify);

        void SaveEventResult(Event finishedEvent);
        Event GetEventById(int eventId, bool eagerLoad = false);        
        List<Event> GetTodayEvents();
        List<Event> GetAllEvents();
        List<Event> GetEventsByDate(DateTime eventDate);
    }
}

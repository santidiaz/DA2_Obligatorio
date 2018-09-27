using BusinessEntities;
using DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class EventPersistance : IEventPersistance
    {
        public void AddEvent(Event newEvent)
        {
            using (Context context = new Context())
            {
                context.Events.Add(newEvent);
                context.SaveChanges();
            }
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events;
            using (Context context = new Context())
            {
                events = (from anEvent in context.Events.OfType<Event>().Include("Teams")
                        select anEvent).ToList();
            }
            return events;
        }

        public List<Event> GetTodayEvents()
        {
            List<Event> todaysEvents;
            using (Context context = new Context())
            {
                todaysEvents = (from anEvent in context.Events.OfType<Event>().Include("Teams")
                                where anEvent.InitialDate.Equals(DateTime.Today)
                                select anEvent).ToList();
            }
            return todaysEvents;
        }
    }
}

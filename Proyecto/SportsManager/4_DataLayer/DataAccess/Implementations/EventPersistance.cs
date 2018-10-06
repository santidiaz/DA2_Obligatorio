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
                context.Teams.Attach(newEvent.Away);
                context.Teams.Attach(newEvent.Local);
                context.Sports.Attach(newEvent.Sport);
                context.Events.Add(newEvent);
                context.SaveChanges();
            }
        }

        public void DeleteEvent(Event eventToBeDeleted)
        {
            using (Context context = new Context())
            {
                context.Events.Attach(eventToBeDeleted);
                context.Events.Remove(eventToBeDeleted);
                context.SaveChanges();
            }
        }

        public Event GetEventById(int eventId, bool eagerLoad = false)
        {
            Event foundEvent;
            using (Context context = new Context())
            {
                if (eagerLoad)
                {
                    foundEvent = context.Events.OfType<Event>()
                        .Include(e => e.Away)
                        .Include(e => e.Local)
                        .Include(e => e.Sport)
                        .Include(e => e.Sport.Teams)
                        .Include(e => e.Comments)
                    .FirstOrDefault(e => e.EventOID.Equals(eventId));
                }
                else
                {
                    foundEvent = context.Events.OfType<Event>()
                    .FirstOrDefault(e => e.EventOID.Equals(eventId));
                }
            }
            return foundEvent;
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events;
            using (Context context = new Context())
            {
                events = (from anEvent in context.Events.OfType<Event>().Include(t => t.Away).Include(t => t.Local)
                          select anEvent).ToList();
            }
            return events;
        }

        public List<Event> GetTodayEvents()
        {
            List<Event> todaysEvents;
            using (Context context = new Context())
            {
                todaysEvents = (from anEvent in context.Events.OfType<Event>()
                                where anEvent.InitialDate.Equals(DateTime.Today)
                                select anEvent).ToList();
            }
            return todaysEvents;
        }

        public List<Event> GetEventsByDate(DateTime eventDate)
        {
            List<Event> events;
            using (Context context = new Context())
            {
                events = (from anEvent in context.Events.OfType<Event>()
                          .Include(e => e.Away)
                          .Include(e => e.Local)
                                where anEvent.InitialDate.Date.Equals(eventDate.Date)
                                select anEvent).ToList();
            }
            return events;
        }

        public void ModifyEvent(Event eventToModify)
        {
            using (Context context = new Context())
            {
                var eventOnDB = context.Events
                    .Include(e => e.Away)
                    .Include(e => e.Local)
                    .Where(e => e.EventOID.Equals(eventToModify.EventOID)).FirstOrDefault();

                eventOnDB.Local = eventToModify.Local;
                eventOnDB.Away = eventToModify.Away;
                eventOnDB.InitialDate = eventToModify.InitialDate;

                context.SaveChanges();
            }
        }
    }
}

using BusinessEntities;
using BusinessEntities.JoinEntities;
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
        //private Context currentContext;
        //public EventPersistance(Context context)
        //{
        //    currentContext = context;
        //}

        public void AddEvent(Event newEvent)
        {
            using (Context context = new Context())
            {
                context.Sports.Attach(newEvent.Sport);
                context.Set<Event>().Add(newEvent);
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



        public void SaveEventResult(Event finishedEvent)
        {
            using (Context context = new Context())
            {
                context.Events.Attach(finishedEvent);

                //context.EventResults.Attach(finishedEvent.Result);
                //foreach(var result in finishedEvent.Result.TeamsResult)
                //    context.TeamResults.Attach(result);

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
                    var eventTeams = context.EventTeams.OfType<EventTeam>()
                        .Include(et => et.Team)
                        .Where(et => et.EventId.Equals(eventId))
                        .ToList();

                    foundEvent = context.Events.OfType<Event>()
                        .Include(e => e.Result)
                        .Include(e => e.Sport)
                        .Include(e => e.Comments)
                    .FirstOrDefault(e => e.Id.Equals(eventId));

                    foundEvent.EventTeams = eventTeams;
                }
                else
                {
                    foundEvent = context.Events.OfType<Event>()
                    .FirstOrDefault(e => e.Id.Equals(eventId));
                }
            }
            return foundEvent;
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events;
            using (Context context = new Context())
            {
                events = context.Events.OfType<Event>()
                    .Include(e => e.EventTeams)
                    .Include(e => e.Result)
                    .Include(e => e.Sport)
                    .Include(e => e.Comments)
                    .OrderBy(e => e.InitialDate).ToList();

                // Add event teams.
                events.ForEach(e =>
                {
                    e.EventTeams = context.EventTeams.OfType<EventTeam>()
                        .Include(et => et.Team)
                        .Where(et => et.EventId.Equals(e.Id))
                        .ToList();
                });
            }
            return events;
        }

        public List<Event> GetTodayEvents()
        {
            List<Event> todaysEvents;
            using (Context context = new Context())
            {
                todaysEvents = (from anEvent in context.Events.OfType<Event>()
                                .Include(e => e.EventTeams)
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
                events = context.Events.OfType<Event>()
                    .Include(e => e.EventTeams)
                    .Where(e => e.InitialDate.Date.Equals(eventDate.Date))
                    .ToList();
            }
            return events;
        }

        public void ModifyEvent(Event eventToModify)
        {
            using (Context context = new Context())
            {
                Event eventOnDB = context.Events
                    .Include(e => e.EventTeams)
                    .Where(e => e.Id.Equals(eventToModify.Id))
                    .FirstOrDefault();

                eventOnDB.EventTeams = eventToModify.EventTeams;
                eventOnDB.InitialDate = eventToModify.InitialDate;
                context.SaveChanges();
            }
        }
    }
}

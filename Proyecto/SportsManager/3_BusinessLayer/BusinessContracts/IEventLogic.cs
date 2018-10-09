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
        Event GetEventById(int eventId);
        void DeleteEventById(int eventId);
        void ModifyEvent(int eventId, string localTeamName, string awayTeamName, DateTime initialDate);
        List<Event> GetAllEvents();
    }
}

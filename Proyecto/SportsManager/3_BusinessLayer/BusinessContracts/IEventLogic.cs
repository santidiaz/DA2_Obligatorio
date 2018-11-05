﻿using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface IEventLogic
    {
        void AddEvent(string sportName, List<string> teamNames, DateTime eventDate);
        void DeleteEventById(int eventId);
        void ModifyEvent(int eventId, List<string> teamNames, DateTime newDate);
        List<Team> FindSportTeams(Sport foundSport, List<string> teamNames);
        Event GetEventById(int eventId, bool eagerLoad = true);
        List<Event> GetAllEvents();
    }
}

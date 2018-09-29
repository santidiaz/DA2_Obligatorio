﻿using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ITeamPersistance
    {
        void AddTeam(Team newTeam);
        List<Team> GetTeams();
        void ModifyTeamByName(string name, Team newTeam);
        Team GetTeamByName(string name);
        void DeleteTeamByName(Team team);
        bool IsTeamInSystem(Team team);
        List<Event> GetEventsByTeam(Team team);
    }
}

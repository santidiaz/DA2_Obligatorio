using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ITeamPersistance
    {
        void AddTeam(Team newTeam, int sportOID);
        void ModifyTeam(Team teamWithModifications);
        void DeleteTeamByName(Team team);
        List<Team> GetTeams(string teamName, bool orderAsc);        
        Team GetTeamByName(string name);
        bool IsTeamInSystem(Team team);
        List<Event> GetEventsByTeam(Team team);
        Team GetTeamById(int teamId);
        bool ValidateTeamOnEvents(Team team);
    }
}
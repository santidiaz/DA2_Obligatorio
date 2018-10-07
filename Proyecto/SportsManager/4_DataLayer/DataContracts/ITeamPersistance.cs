using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ITeamPersistance
    {
        void AddTeam(Team newTeam, int sportOID);
        List<Team> GetTeams(bool asc, string teamName);
        void ModifyTeam(Team teamWithModifications);
        Team GetTeamByName(string name);
        void DeleteTeamByName(Team team);
        bool IsTeamInSystem(Team team);
        List<Event> GetEventsByTeam(Team team);
        Team GetTeamByOID(int oid);
        bool ValidateTeamOnEvents(Team team);
    }
}
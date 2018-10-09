using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ITeamLogic
    {
        void AddTeam(Team newTeam, int sportOID);
        List<Team> GetTeams(bool asc, string teamName);
        void ModifyTeamByName(string name, Team team);
        Team GetTeamByName(string name);
        Team GetTeamByOID(int oid);
        bool DeleteTeamByName(string name);
        List<Event> GetEventsByTeam(string team);
    }
}
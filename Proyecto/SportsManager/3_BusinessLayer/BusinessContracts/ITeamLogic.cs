using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ITeamLogic
    {
        void AddTeam(Team newTeam, int Id);
        List<Team> GetTeams(string teamName, bool orderAsc);
        void ModifyTeamByName(string name, Team team);
        Team GetTeamByName(string name);
        Team GetTeamById(int teamId);
        void DeleteTeamByName(string name);
        List<Event> GetEventsByTeam(string team);
    }
}
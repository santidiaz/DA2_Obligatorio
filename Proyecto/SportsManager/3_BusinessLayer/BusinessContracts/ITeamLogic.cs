using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ITeamLogic
    {
        void AddTeam(Team newTeam);
        List<Team> GetTeams();
        void ModifyTeamByName(string name, Team team);
        Team GetTeamByName(string name);
        bool DeleteTeamByName(string name);
        List<Event> GetEventsByTeam(string team);
    }
}

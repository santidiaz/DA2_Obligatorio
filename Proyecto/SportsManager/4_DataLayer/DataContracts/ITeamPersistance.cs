using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ITeamPersistance
    {
        void AddTeam(Team newTeam);
        List<Team> GetTeams();
        void ModifyTeamByName(Team newTeam);
        Team GetTeamByName(string name);
        void DeleteTeamByName(string name);
        bool IsTeamInSystem(Team team);
    }
}

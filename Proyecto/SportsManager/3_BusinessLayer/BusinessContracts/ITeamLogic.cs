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
    }
}

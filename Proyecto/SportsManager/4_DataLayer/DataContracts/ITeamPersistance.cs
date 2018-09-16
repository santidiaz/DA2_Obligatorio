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
    }
}

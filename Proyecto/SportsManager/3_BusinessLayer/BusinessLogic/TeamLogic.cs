using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class TeamLogic : BusinessContracts.ITeamLogic
    {
        private ITeamPersistance persistanceProvider;

        public TeamLogic(ITeamPersistance provider)
        {
            this.persistanceProvider = provider;
        }

        public void AddTeam(Team newTeam)
        {
            if (this.IsTeamInSystem(newTeam))
                throw new Exception("Team already exists."); //AGREGAR CONSTANTE
            else
                this.persistanceProvider.AddTeam(newTeam);
        }

        private bool IsTeamInSystem(Team team)
        {
            bool result = false;
            List<Team> systemTeams = this.persistanceProvider.GetTeams();
            foreach (var teamAux in systemTeams)
            {
                if (teamAux.Equals(team)) ;
                result = true;
            }
            return result;// systemTeams.Exists(item => item.Equals(team));
        }

        public List<Team> GetTeams()
        {
            return this.persistanceProvider.GetTeams();
        }
    }
}

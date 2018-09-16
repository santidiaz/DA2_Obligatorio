using BusinessEntities;
using CommonUtilities;
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
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS);
            else
                this.persistanceProvider.AddTeam(newTeam);
        }

        private bool IsTeamInSystem(Team team)
        {
            bool result = false;
            List<Team> systemTeams = this.persistanceProvider.GetTeams();
            foreach (var teamAux in systemTeams)
            {
                if (teamAux.Equals(team)) { result = true; };
                
            }
            return result;// systemTeams.Exists(item => item.Equals(team));
        }

        public List<Team> GetTeams()
        {
            return this.persistanceProvider.GetTeams();
        }

        public void ModifyTeamByName(string name, Team team)
        {
            if (this.IsTeamInSystem(new Team() { Name = name }))
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS);
            else
                this.persistanceProvider.ModifyTeamByName(team);
        }

        public Team GetTeamByName(string name)
        {
            var team = this.persistanceProvider.GetTeamByName(name);
            
            if (team == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS);
            return team;
        }

        public void DeleteTeamByName(string name)
        {
            var systemTeams = this.persistanceProvider.GetTeams();
            var teamToDelete = systemTeams.Find(t => t.Name == name);
            if (teamToDelete == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS);
            this.persistanceProvider.DeleteTeamByName(name);
        }
    }
}

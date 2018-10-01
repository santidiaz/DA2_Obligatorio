using BusinessContracts;
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
            Team teamToModify = this.GetTeamByName(name);
            if (teamToModify == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS);
            else
                this.persistanceProvider.ModifyTeamByName(name, team);
        }

        public Team GetTeamByName(string name)
        {
            var team = this.persistanceProvider.GetTeamByName(name);

            if (team == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS);
            return team;
        }

        public bool DeleteTeamByName(string name)
        {
            try
            {
                bool result = true;
                Team teamToDelete = this.GetTeamByName(name);

                if (teamToDelete != null)
                    this.persistanceProvider.DeleteTeamByName(teamToDelete);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ex);
            }
        }
        
        public List<Event> GetEventsByTeam(string teamName)
        {
            try
            {
                List<Event> events = new List<Event>();
                Team team = this.GetTeamByName(teamName);

                if (team != null)
                    return this.persistanceProvider.GetEventsByTeam(team);
                else
                    return events;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS, ex);
            }
        }
    }
}
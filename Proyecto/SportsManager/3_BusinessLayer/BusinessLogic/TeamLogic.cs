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
        private ITeamPersistance persistanceProvideTeam;
        private ISportPersistance persistanceProvideSport;

        public TeamLogic(ITeamPersistance provider, ISportPersistance providerSport)
        {
            this.persistanceProvideTeam = provider;
            this.persistanceProvideSport = providerSport;
        }

        public void AddTeam(Team newTeam, int sportOID)
        {
            if (this.IsTeamInSystem(newTeam))
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS);
            else if (sportOID == null || sportOID <= 0)
                throw new Exception(Constants.TeamErrors.TEAM_SPORTOID_FAIL);
            else if (this.persistanceProvideSport.GetSports().Find(s => s.SportOID == sportOID) == null)
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS);
            else
                this.persistanceProvideTeam.AddTeam(newTeam, sportOID);
        }

        private bool IsTeamInSystem(Team team)
        {
            bool result = false;
            List<Team> systemTeams = this.persistanceProvideTeam.GetTeams();
            foreach (var teamAux in systemTeams)
            {
                if (teamAux.Equals(team)) { result = true; };

            }
            return result;// systemTeams.Exists(item => item.Equals(team));
        }

        public List<Team> GetTeams()
        {
            return this.persistanceProvideTeam.GetTeams();
        }

        public void ModifyTeamByName(string name, Team team)
        {
            Team teamToModify = this.GetTeamByName(name);
            if (teamToModify == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS);
            else
                team.TeamOID = teamToModify.TeamOID;
                this.persistanceProvideTeam.ModifyTeamByName(name, team);
        }

        public Team GetTeamByName(string name)
        {
            var team = this.persistanceProvideTeam.GetTeamByName(name);

            if (team == null)
                throw new Exception(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS);
            return team;
        }

        public Team GetTeamByOID(int oid)
        {
            var team = this.persistanceProvideTeam.GetTeamByOID(oid);

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
                    this.persistanceProvideTeam.DeleteTeamByName(teamToDelete);
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
                    return this.persistanceProvideTeam.GetEventsByTeam(team);
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
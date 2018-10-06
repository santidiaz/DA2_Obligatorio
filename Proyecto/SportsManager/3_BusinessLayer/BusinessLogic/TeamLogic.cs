using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
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
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS, ExceptionStatusCode.Conflict);
            else if (sportOID == null || sportOID <= 0)
                throw new EntitiesException(Constants.TeamErrors.TEAM_SPORTOID_FAIL, ExceptionStatusCode.InvalidData);
            else if (this.persistanceProvideSport.GetSports().Find(s => s.SportOID == sportOID) == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS, ExceptionStatusCode.Conflict);
            else
                this.persistanceProvideTeam.AddTeam(newTeam, sportOID);
        }

        private bool IsTeamInSystem(Team team)
        {
            bool result = false;
            List<Team> systemTeams = this.persistanceProvideTeam.GetTeams(true, string.Empty);
            foreach (var teamAux in systemTeams)
            {
                if (teamAux.Equals(team)) { result = true; };

            }
            return result;// systemTeams.Exists(item => item.Equals(team));
        }

        public List<Team> GetTeams(bool asc, string teamName)
        {
            return this.persistanceProvideTeam.GetTeams(asc, teamName);
        }

        public void ModifyTeamByName(string name, Team team)
        {
            Team teamToModify = this.GetTeamByName(name);
            if (teamToModify == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS, ExceptionStatusCode.Conflict);
            else
                team.TeamOID = teamToModify.TeamOID;
                this.persistanceProvideTeam.ModifyTeamByName(name, team);
        }

        public Team GetTeamByName(string name)
        {
            var team = this.persistanceProvideTeam.GetTeamByName(name);

            if (team == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);
            
            return team;
        }

        public Team GetTeamByOID(int oid)
        {
            var team = this.persistanceProvideTeam.GetTeamByOID(oid);

            if (team == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);
            return team;
        }

        public bool DeleteTeamByName(string name)
        {
            try
            {
                bool result = true;
                Team teamToDelete = this.GetTeamByName(name);

                if (teamToDelete != null && !this.ValidateTeamOnEvents(teamToDelete))
                    this.persistanceProvideTeam.DeleteTeamByName(teamToDelete);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);
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
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS, ExceptionStatusCode.NotFound);
            }
        }

        public bool ValidateTeamOnEvents(Team team)
        {
            return persistanceProvideTeam.ValidateTeamOnEvents(team);
        }

    }
}
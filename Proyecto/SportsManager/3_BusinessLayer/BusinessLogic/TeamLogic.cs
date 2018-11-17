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
    public class TeamLogic : ITeamLogic
    {
        private ITeamPersistance teamPersistance;
        private ISportPersistance sportPersistance;

        public TeamLogic(ITeamPersistance teamProvider, ISportPersistance sportProvider)
        {
            this.teamPersistance = teamProvider;
            this.sportPersistance = sportProvider;
        }

        #region Public methods
        public void AddTeam(Team newTeam, int Id)
        {
            Sport foundSport = sportPersistance.GetSportById(Id, true);
            if(foundSport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.InvalidData);

            if (this.IsTeamInSystem(newTeam))
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS, ExceptionStatusCode.Conflict);
            
            this.teamPersistance.AddTeam(newTeam, Id);
        }

        public List<Team> GetTeams(string teamName, bool orderAsc)
        {
            return this.teamPersistance.GetTeams(teamName, orderAsc);
        }

        public void ModifyTeamByName(string teamOldName, Team teamWithModifications)
        {
            Team teamToModify = this.GetTeamByName(teamOldName);

            teamWithModifications.Id = teamToModify.Id;
            this.teamPersistance.ModifyTeam(teamWithModifications);
        }

        public Team GetTeamByName(string name)
        {
            Team team = this.teamPersistance.GetTeamByName(name);
            if (team == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return team;
        }

        public Team GetTeamById(int teamId)
        {
            Team team = this.teamPersistance.GetTeamById(teamId);
            if (team == null)
                throw new EntitiesException(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return team;
        }

        public void DeleteTeamByName(string name)
        {
            try
            {
                Team teamToDelete = this.GetTeamByName(name);

                if (teamToDelete != null && !this.ValidateTeamOnEvents(teamToDelete))
                    this.teamPersistance.DeleteTeamByName(teamToDelete);
                else
                    throw new EntitiesException(Constants.TeamErrors.TEAM_EXISTES_ON_EVENT, ExceptionStatusCode.Conflict);
            }
            catch (EntitiesException)
            {
                throw new EntitiesException(Constants.TeamErrors.TEAM_EXISTES_ON_EVENT, ExceptionStatusCode.Conflict);
            }
            catch (Exception)
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

                return this.teamPersistance.GetEventsByTeam(team);
            }
            catch (Exception)
            {
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);
            }
        }
        #endregion

        #region Private metods
        private bool IsTeamInSystem(Team team)
        {
            return teamPersistance.GetTeamByName(team.Name) != null;
        }
        private bool ValidateTeamOnEvents(Team team)
        {
            return teamPersistance.ValidateTeamOnEvents(team);
        }
        #endregion
    }
}
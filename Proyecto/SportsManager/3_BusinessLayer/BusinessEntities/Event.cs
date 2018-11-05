using BusinessEntities.Exceptions;
using BusinessEntities.JoinEntities;
using CommonUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Event
    {
        #region Private Attributes
        private DateTime _initialDate;
        private List<EventTeam> _teams;
        private List<Comment> _coments;
        #endregion

        public int EventOID { get; set; } // [Object Id] This id is required by EntityFramework.
        public DateTime InitialDate
        {
            get { return this._initialDate; }
            set
            {
                if (value.Date < DateTime.Now.Date)
                    throw new EntitiesException(Constants.EventError.INVALID_DATE, ExceptionStatusCode.InvalidData);

                this._initialDate = value;
            }
        }
        public bool MultipleTeamsEvent { get; set; }
        public Sport Sport { get; set; }
        public virtual List<EventTeam> EventTeams
        {
            get { return this._teams; }
            set
            {
                List<Team> teams = value?.Select(v => v.Team).ToList();
                if (teams != null && this.TeamsQuantityIsValid(teams))
                    throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

                this._teams = value;
            }
        }

        public virtual List<Comment> Comments
        {
            get { return this._coments?.OrderByDescending(c => c.DatePosted)?.ToList(); }
            set { this._coments = value; }
        }

        public Event()
        {
            this._coments = new List<Comment>();
            this._teams = new List<EventTeam>();
            this._initialDate = DateTime.MinValue;;
        }
        public Event(DateTime date, Sport sport, List<Team> teams)
        {
            this._coments = new List<Comment>();
            this.InitialDate = date;
            this.Sport = sport;
            this.MultipleTeamsEvent = sport.AllowdMultipleTeamsEvents;            
            this.LoadTeams(teams);
        }        

        #region Public methods
        public Team GetLocalTeam()
        {
            return this.EventTeams[0].Team;
        }

        public Team GetAwayTeam()
        {
            return this.EventTeams[1].Team;
        }
        
        public void ModifyTeams(List<Team> newTeams)
        {
            bool result = this.TeamsQuantityIsValid(newTeams);
            if(!result)
                throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

            if (this.MultipleTeamsEvent)
            {
                this.LoadTeams(newTeams);
            }
            else
            {
                Team local = newTeams.ElementAt(0);
                Team away = newTeams.ElementAt(1);
                if (this.AreValidTeams(local, away))
                {
                    this.SetLocal(local);
                    this.SetAway(away);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Event)
                return this.EventOID.Equals(((Event)obj).EventOID);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Private methods
        private void SetLocal(Team localTeam)
        {
            this.EventTeams[0].Team = localTeam;
        }
        private void SetAway(Team awayTeam)
        {
            this.EventTeams[1].Team = awayTeam;
        }
        private bool AreValidTeams(Team local, Team away)
        {
            return local != null && away != null
                && !local.Equals(away);
        }
        private void LoadTeams(List<Team> teams)
        {
            if (teams != null && this.TeamsQuantityIsValid(teams))
                throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

            this._teams = new List<EventTeam>();
            foreach (Team currentTeam in teams)
            {
                this._teams.Add(
                    new EventTeam
                    {
                        Team = currentTeam,
                        TeamOID = currentTeam.TeamOID,
                        EventOID = this.EventOID
                    });
            }
        }
        // Teams must be 2, or if SPORT allow multipleTeamsEvents, count must be 3 or more.
        private bool TeamsQuantityIsValid(List<Team> teams)
        {
            return teams == null || teams.Count() < 2 || (!this.MultipleTeamsEvent && teams.Count() > 2);
        }
        #endregion
    }
}
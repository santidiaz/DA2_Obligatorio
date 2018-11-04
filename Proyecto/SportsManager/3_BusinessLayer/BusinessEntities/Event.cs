using BusinessEntities.Exceptions;
using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Event
    {
        #region Private Attributes
        private DateTime _initialDate;
        private List<Team> _teams;
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
        public List<Team> Teams
        {
            get { return this._teams; }
            set
            {
                if (this.TeamsQuantityIsValid(value))
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
            this._teams = new List<Team>();
            this._initialDate = DateTime.MinValue;;
        }
        public Event(DateTime date, Sport sport, List<Team> teams)
        {
            this._coments = new List<Comment>();
            this.InitialDate = date;
            this.Sport = sport;
            this.MultipleTeamsEvent = sport.AllowdMultipleTeamsEvents;
            this.Teams = teams;
        }

        #region Public methods
        public Team GetLocalTeam()
        {
            return this.Teams.ElementAt(0);
        }

        public Team GetAwayTeam()
        {
            return this.Teams.ElementAt(1);
        }
        
        public void ModifyTeams(List<Team> newTeams)
        {
            bool result = this.TeamsQuantityIsValid(newTeams);
            if(!result)
                throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

            if (this.MultipleTeamsEvent)
            {
                this.Teams = newTeams;
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
            this.Teams[0] = localTeam;
        }
        private void SetAway(Team awayTeam)
        {
            this.Teams[1] = awayTeam;
        }
        private bool AreValidTeams(Team local, Team away)
        {
            return local != null && away != null
                && !local.Equals(away);
        }

        // Teams must be 2, or if SPORT allow multipleTeamsEvents, count must be 3 or more.
        private bool TeamsQuantityIsValid(List<Team> teams)
        {
            return teams == null || teams.Count < 2 || (!this.MultipleTeamsEvent && teams.Count > 2);
        }
        #endregion
    }
}
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

        public int Id { get; set; } 
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
                if (teams != null && this.TeamsQuantityIsInvalid(teams))
                    throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

                this._teams = value;
            }
        }
        public virtual List<Comment> Comments
        {
            get { return this._coments; }
            set { this._coments = value; }
        }
        public EventResult Result { get; set; }

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
            if (this.TeamsQuantityIsInvalid(newTeams))
                throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

            if (this.MultipleTeamsEvent)
                this.LoadTeams(newTeams);
            else
                this.SetupTwoTeamsEvent(newTeams);
        }
        public void AddNewComment(Comment commentToAdd)
        {
            this._coments.Add(commentToAdd);
        }
        public List<Comment> GetOrderedCommentsDesc()
        {
            return this._coments?.OrderByDescending(c => c.DatePosted)?.ToList();
        }
        public bool HasResult()
        {
            return this.Result != null;
        }
        public override bool Equals(object obj)
        {
            if (obj is Event)
                return this.Id.Equals(((Event)obj).Id);
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Private methods
        private void LoadTeams(List<Team> teams)
        {
            if (teams != null && this.TeamsQuantityIsInvalid(teams))
                throw new EntitiesException(Constants.EventError.INVALID_AMOUNT_OF_TEAMS, ExceptionStatusCode.InvalidData);

            this._teams = new List<EventTeam>();
            foreach (Team currentTeam in teams)
            {
                this._teams.Add(
                    new EventTeam
                    {
                        Team = currentTeam,
                        TeamId = currentTeam.Id,
                        EventId = this.Id
                    });
            }
        }
        private void SetupTwoTeamsEvent(List<Team> teams)
        {
            Team local = teams.ElementAt(0);
            Team away = teams.ElementAt(1);
            if (this.AreValidTeams(local, away))
            {
                this.SetLocal(local);
                this.SetAway(away);
            }
        }
        private bool AreValidTeams(Team local, Team away)
        {
            return local != null && away != null
                && !local.Equals(away);
        }
        private void SetLocal(Team localTeam)
        {
            this.EventTeams[0].Team = localTeam;
        }
        private void SetAway(Team awayTeam)
        {
            this.EventTeams[1].Team = awayTeam;
        }

        // Teams must be 2, or if SPORT allow multipleTeamsEvents, count must be 3 or more.
        private bool TeamsQuantityIsInvalid(List<Team> teams)
        {
            return teams == null || teams.Count() < 2 || (!this.MultipleTeamsEvent && teams.Count() > 2);
        }
        #endregion
    }
}
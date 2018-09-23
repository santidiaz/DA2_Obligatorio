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
        private Team[] _teams;
        private List<Comment> _comments;
        #endregion

        #region Public Attributes
        public int EventOID { get; set; } // [Object Id] This id is Event by EntityFramework.
        public DateTime InitialDate
        {
            get { return this._initialDate; }
            set
            {
                if (value < DateTime.Now)
                    throw new EntitiesException(Constants.EventError.INVALID_DATE, ExceptionStatusCode.InvalidData);

                this._initialDate = value;
            }
        }
        public Sport Sport { get; set; }
        #endregion

        public Event()
        {
            this._comments = new List<Comment>();
            this._teams = new Team[2];
            this._initialDate = DateTime.Now;
        }

        public Event(DateTime date, Sport sport, Team firstTeam, Team secondTeam)
        {
            this._comments = new List<Comment>();
            this._teams = new Team[2] { firstTeam, secondTeam };
            this.InitialDate = date;
            this.Sport = sport;
        }

        #region Public methods
        public Team[] GetTeams()
        {
            return this._teams;
        }

        public Team GetFirstTeam()
        {
            return this._teams[0];
        }

        public Team GetSecondTeam()
        {
            return this._teams[1];
        }

        public List<Comment> GetComments()
        {
            return this._comments.OrderByDescending(c => c.DatePosted).ToList();
        }

        public void AddComment(Comment newComment)
        {
            this._comments.Add(newComment);
        }

        public bool ModifyTeams(Team firstTeam, Team secondTeam)
        {
            bool result = false;
            if (this.AreValidTeams(firstTeam, secondTeam))
            {
                this._teams[0] = firstTeam;
                this._teams[1] = secondTeam;
                result = true;
            }
            return result;
        }
        #endregion

        #region Private methods
        private bool AreValidTeams(Team firstTeam, Team secondTeam)
        {
            bool result = firstTeam != null && secondTeam != null
                && !firstTeam.Equals(secondTeam);

            // If validations are true so far, 
            // i check that the 1st team belong to the sport
            result = result ? this.Sport.TeamsList
                       .Exists(t => t.Name.Equals(firstTeam.Name)) : result;

            // If validations are true so far, 
            // i check that the 2nd team belong to the sport
            result = result ? this.Sport.TeamsList
                   .Exists(t => t.Name.Equals(secondTeam.Name)) : result;

            return result;
        }
        #endregion
    }
}
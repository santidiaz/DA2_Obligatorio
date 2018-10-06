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
        #endregion

        #region Public Attributes
        public int EventOID { get; set; } // [Object Id] This id is Event by EntityFramework.
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
        public Sport Sport { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public Team Local { get; set; }
        public Team Away { get; set; }
        #endregion

        public Event()
        {
            this.Comments = new List<Comment>();
            this._initialDate = DateTime.Now;
        }

        public Event(DateTime date, Sport sport, Team localTeam, Team awayTeam)
        {
            this.Comments = new List<Comment>();
            this.Local = localTeam;
            this.Away = awayTeam;
            this.InitialDate = date;
            this.Sport = sport;
        }

        #region Public methods
        public Team GetLocalTeam()
        {
            return this.Local;
        }

        public Team GetAwayTeam()
        {
            return this.Away;
        }

        public List<Comment> GetComments()
        {
            return this.Comments.OrderByDescending(c => c.DatePosted).ToList();
        }

        public void AddComment(Comment newComment)
        {
            this.Comments.Add(newComment);
        }

        public bool ModifyTeams(Team localTeam, Team awayTeam)
        {
            bool result = false;
            if (this.AreValidTeams(localTeam, awayTeam))
            {
                this.Local = localTeam;
                this.Away = awayTeam;
                result = true;
            }
            return result;
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
        private bool AreValidTeams(Team localTeam, Team awayTeam)
        {
            bool result = localTeam != null && awayTeam != null
                && !localTeam.Equals(awayTeam);

            // If validations are true so far, 
            // i check that the 1st team belong to the sport
            result = result ? this.Sport.Teams
                       .Exists(t => t.Name.Equals(localTeam.Name)) : result;

            // If validations are true so far, 
            // i check that the 2nd team belong to the sport
            result = result ? this.Sport.Teams
                   .Exists(t => t.Name.Equals(awayTeam.Name)) : result;

            return result;
        }
        #endregion
    }
}
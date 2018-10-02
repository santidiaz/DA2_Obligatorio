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
        public virtual Team[] Teams { get; set; }
        #endregion

        public Event()
        {
            this.Comments = new List<Comment>();
            this.Teams = new Team[2];
            this._initialDate = DateTime.Now;
        }

        public Event(DateTime date, Sport sport, Team firstTeam, Team secondTeam)
        {
            this.Comments = new List<Comment>();
            this.Teams = new Team[2] { firstTeam, secondTeam };
            this.InitialDate = date;
            this.Sport = sport;
        }

        #region Public methods
        public Team[] GetTeams()
        {
            return this.Teams;
        }

        public Team GetFirstTeam()
        {
            return this.Teams[0];
        }

        public Team GetSecondTeam()
        {
            return this.Teams[1];
        }

        public List<Comment> GetComments()
        {
            return this.Comments.OrderByDescending(c => c.DatePosted).ToList();
        }

        public void AddComment(Comment newComment)
        {
            this.Comments.Add(newComment);
        }

        public bool ModifyTeams(Team firstTeam, Team secondTeam)
        {
            bool result = false;
            if (this.AreValidTeams(firstTeam, secondTeam))
            {
                this.Teams[0] = firstTeam;
                this.Teams[1] = secondTeam;
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
        #endregion

        #region Private methods
        private bool AreValidTeams(Team firstTeam, Team secondTeam)
        {
            bool result = firstTeam != null && secondTeam != null
                && !firstTeam.Equals(secondTeam);

            // If validations are true so far, 
            // i check that the 1st team belong to the sport
            result = result ? this.Sport.Teams
                       .Exists(t => t.Name.Equals(firstTeam.Name)) : result;

            // If validations are true so far, 
            // i check that the 2nd team belong to the sport
            result = result ? this.Sport.Teams
                   .Exists(t => t.Name.Equals(secondTeam.Name)) : result;

            return result;
        }
        #endregion
    }
}
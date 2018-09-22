using BusinessEntities.Exceptions;
using CommonUtilities;
using System;
using System.Collections.Generic;
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

        public Team[] GetTeams()
        {
            return this._teams;
        }

        public List<Comment> GetComments()
        {
            return this._comments;
        }
        /*
        + Team():
        + Team(date:DateTime, sport:Sport, firstTeam:Team, secondTeam:Team)
        + GetTeams(): Team[2]
        + AddTeams(firstTeam:Team, secondTeam:Team)
        + ModifyTeams(firstTeam:Team, secondTeam:Team)
        + AddComment(comment:string, userName:string)
         */
    }
}

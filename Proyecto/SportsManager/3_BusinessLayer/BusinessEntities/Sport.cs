using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Sport
    {
        #region Private attributes
        private string _name;
        private List<Team> _teamList;
        #endregion
        public int SportOID { get; set; } // [Object Id] This id is team by EntityFramework.

        public Sport()
        {

        }
        public Sport(string expectedName, List<Team> expectedListTeam)
        {
            this.Name = expectedName;
            this.TeamsList = expectedListTeam;
        }

        public string Name
        {
            get { return this._name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.SportErrors.NAME_REQUIRED);

                this._name = value;
            }
        }
        public List<Team> TeamsList
        {
            get { return this._teamList; }
            set
            {
                if (value == null || value.Count == 0)
                    throw new Exception(Constants.SportErrors.TEAMLIST_REQUIRED);

                this._teamList = value;
            }
        }
    }
}

﻿using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Sport
    {
        #region Private attributes
        private string _name;
        private List<Team> _teams;
        #endregion

        public int SportOID { get; set; } // [Object Id] This id is team by EntityFramework.
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
        public List<Team> Teams
        {
            get { return this._teams; }
            set
            {
                if (value == null || value.Count == 0)
                    throw new Exception(Constants.SportErrors.TEAMLIST_REQUIRED);

                this._teams = value;
            }
        }

        public Sport()
        {
            this._name = string.Empty;
            this._teams = new List<Team>();
        }
        public Sport(string newName, List<Team> newTeams)
        {
            this.Name = newName;
            this.Teams = newTeams;
        }
    }
}
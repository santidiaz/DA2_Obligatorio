using BusinessEntities.JoinEntities;
using CommonUtilities;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class User
    {
        #region Private attributes
        private string _name;
        private string _lastName;
        private string _userName;
        private string _email;
        private string _password;
        #endregion

        #region Public attributes
        public int UserOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public Guid Token { get; set; }
        public string Name
        {
            get { return this._name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.UserError.NAME_REQUIRED);

                this._name = value;
            }
        }
        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.UserError.LAST_NAME_REQUIRED);

                this._lastName = value;
            }
        }
        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.UserError.USER_NAME_REQUIRED);

                this._userName = value;
            }
        }
        public string Email
        {
            get { return this._email; }
            set
            {
                if (this.IsValidEmail(value))
                    this._email = value;
            }
        }
        public bool IsAdmin { get; set; } = false;
        public string Password
        {
            get { return this._password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.UserError.PASSWORD_REQUIRED);

                this._password = value;
            }
        }
        //public virtual List<Team> FavouriteTeams { get; set; }// Virtual implies that the list will be lazy-loaded unless you specifically mark them otherwise.

        public virtual List<UserTeam> FavouriteTeams { get; set; }
        #endregion

        #region Constructors
        public User()
        {
            this._name = string.Empty;
            this._lastName = string.Empty;
            this._userName = string.Empty;
            this._email = string.Empty;
            this._password = string.Empty;
            this.IsAdmin = false;
            this.FavouriteTeams = new List<UserTeam>();
        }
        public User(string name, string lastName, string userName, string password, string email, bool isAdmin = false)
        {
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.FavouriteTeams = new List<UserTeam>();
            this.IsAdmin = isAdmin;
        }
        #endregion

        #region Methods
        public void AddFavouriteTeam(UserTeam newTeam)
        {
            if (!this.FavouriteTeams.Contains(newTeam))
                this.FavouriteTeams.Add(newTeam);
        }
        #endregion

        #region Private Methods
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address.Equals(email);
            }
            catch
            {
                throw new Exception(Constants.UserError.INVALID_EMAIL_FORMAT);
            }
        }
        #endregion 
    }
}
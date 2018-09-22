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
        private List<Team> favouriteTeams;
        #endregion

        #region Public attributes
        public int UserOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public string Name
        {
            get { return this._name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.Errors.NAME_REQUIRED);

                this._name = value;
            }
        }
        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.Errors.LAST_NAME_REQUIRED);

                this._lastName = value;
            }
        }
        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.Errors.USER_NAME_REQUIRED);

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
        public string  Password
        {
            get { return this._password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.Errors.PASSWORD_REQUIRED);

                this._password = value;
            }
        }
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
            this.favouriteTeams = new List<Team>();
        }
        public User(string name, string lastName, string userName, string password, string email, bool isAdmin = false)
        {
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.favouriteTeams = new List<Team>();
            this.IsAdmin = isAdmin;
        }
        #endregion

        #region Methods
        public List<Team> GetFavouritesTeams()
        {
            return this.favouriteTeams;
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
                throw new Exception(Constants.Errors.INVALID_EMAIL_FORMAT);
            }
        }
        #endregion 
    }
}

using CommonUtilities;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class User
    {
        private string _email;
        private List<Team> _favouriteTeams;

        public int UserOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email
        {
            get { return this._email; }
            set
            {
                if (this.IsValidEmail(value))
                    this._email = value;
            }
        }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;

        public User()
        {
            this.Name = string.Empty;
            this.LastName = string.Empty;
            this.UserName = string.Empty;
            this._email = string.Empty;
            this.Password = string.Empty;
            this._favouriteTeams = new List<Team>();
        }

        public User(string name, string lastName, string userName, string password, string email, bool isAdmin)
        {
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this._favouriteTeams = new List<Team>();
            this.IsAdmin = isAdmin;
        }

        public virtual string GetFullName()
        {
            return string.Format("{0} {1}", this.Name, this.LastName);
        }

        public List<Team> GetFavouritesTeams()
        {
            return this._favouriteTeams;
        }

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
                throw new Exception(Constants.Errors.ERROR_INVALID_EMAIL_FORMAT);
            }
        }
        #endregion 
    }
}

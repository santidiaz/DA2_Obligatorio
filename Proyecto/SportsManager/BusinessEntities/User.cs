using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessEntities
{
    public class User
    {
        private string _email;
        //private List<Team> _favouriteTeams;

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
        public string IsAdmin { get; set; }

        public virtual string GetFullName()
        {
            return string.Format("{0} {1}", this.Name, this.LastName);
        }

        //public List<Team> GetFavouritesTeams() { }

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
                throw new Exception("Invalid email format.");
            }
        }
        #endregion 
    }
}

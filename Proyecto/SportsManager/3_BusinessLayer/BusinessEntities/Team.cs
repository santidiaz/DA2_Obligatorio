using CommonUtilities;
using System;

namespace BusinessEntities
{
    public class Team
    {
        #region Private attributes
        private string _name;
        #endregion

        public int Id { get; set; }
        public string Name
        {
            get { return this._name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.TeamErrors.NAME_REQUIRED);

                this._name = value;
            }
        }
        public byte[] Photo { get; set; }
        
        public Team()
        {
            this._name = string.Empty;
        }
        public Team(string expectedName, byte[] expectedPhoto)
        {
            this.Name = expectedName;
            this.Photo = expectedPhoto;
        }

        public override bool Equals(object obj)
        {
            if (obj is Team)
                return this.Name.Equals(((Team)obj).Name);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
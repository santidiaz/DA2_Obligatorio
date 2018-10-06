using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Team
    {
        #region Private attributes
        private string _name;
        private byte[] _photo;
        #endregion

        public int TeamOID { get; set; } // [Object Id] This id is team by EntityFramework.
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
        public byte[] Photo
        {
            get { return this._photo; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new Exception(Constants.TeamErrors.PHOTO_INVALID);

                this._photo = value;
            }
        } //ver como guardar foto.

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
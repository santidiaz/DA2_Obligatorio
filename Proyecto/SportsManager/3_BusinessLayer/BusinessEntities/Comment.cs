using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Comment
    {
        #region Private attributes
        private string _description;
        private string _creatorName;
        #endregion

        public int CommentOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public string CreatorName
        {
            get { return this._creatorName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.CommentError.CREATORNAME_REQUIRED);

                this._creatorName = value;
            }
        }
        public DateTime DatePosted { get; set; }
        public string Description
        {
            get { return this._description; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(Constants.CommentError.DESCRIPTION_REQUIRED);

                this._description = value;
            }
        }
        
        public Comment()
        {
            this.DatePosted = DateTime.Now;
        }

        public Comment(string newDescription, string creatorName)
        {
            this.DatePosted = DateTime.Now;
            this.Description = newDescription;
            this.CreatorName = creatorName;
        }

        public override bool Equals(object obj)
        {
            if (obj is Comment)
                return this.DatePosted.Equals(((Comment)obj).DatePosted)
                    && this.CreatorName.Equals(CreatorName);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

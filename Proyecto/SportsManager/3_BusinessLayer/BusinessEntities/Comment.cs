using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Comment
    {
        private DateTime _datePosted;

        public int CommentOID { get; set; } // [Object Id] This id is used by EntityFramework.
        private string _description;
        private string _creatorName;
        public DateTime DatePosted
        {
            get { return this._datePosted; }
        }

        public Comment()
        {
            this._datePosted = DateTime.Now;
        }

        public Comment(string exceptedDescription, string exceptedCreatorName)
        {
            this._datePosted = DateTime.Now;
            this.Description = exceptedDescription;
            this.CreatorName = exceptedCreatorName;
        }

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

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Comment
    {
        private DateTime _datePosted;

        public int CommentOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public string Description { get; set; } = string.Empty;
        public string CreatorName { get; set; }
        public DateTime DatePosted
        {
            get { return this._datePosted; }
        }

        public Comment()
        {
            this._datePosted = DateTime.Now;
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

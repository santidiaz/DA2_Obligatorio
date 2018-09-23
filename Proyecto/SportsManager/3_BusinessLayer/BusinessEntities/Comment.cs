using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Comment
    {
        public int CommentOID { get; set; } // [Object Id] This id is used by EntityFramework.
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public DateTime DatePosted { get; set; }

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

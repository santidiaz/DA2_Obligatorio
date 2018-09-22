using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Comment
    {
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public DateTime Date { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Comment)
                return this.Date.Equals(((Comment)obj).Date) 
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

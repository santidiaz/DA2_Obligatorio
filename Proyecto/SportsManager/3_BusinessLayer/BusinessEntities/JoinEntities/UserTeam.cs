using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.JoinEntities
{
    public class UserTeam
    {
        public int UserOID { get; set; }
        public User User{ get; set; }

        public int TeamOID { get; set; }
        public Team Team { get; set; }
    }
}

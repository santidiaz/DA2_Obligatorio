using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.JoinEntities
{
    public class EventTeam
    {
        public int EventOID { get; set; }
        public Event Event { get; set; }

        public int TeamOID { get; set; }
        public Team Team { get; set; }
    }
}

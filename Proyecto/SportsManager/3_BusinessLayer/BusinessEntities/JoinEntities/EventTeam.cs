using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.JoinEntities
{
    public class EventTeam
    {
        public int EventId { get; set; }
        //public Event Event { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}

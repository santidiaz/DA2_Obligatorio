using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Event
    {
        public int EventOID { get; set; } // [Object Id] This id is Event by EntityFramework.
        public DateTime Date { get; set; }
        public Sport Sport { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public string Comments { get; set; } //esto va a ser una lista de Comment
    }
}

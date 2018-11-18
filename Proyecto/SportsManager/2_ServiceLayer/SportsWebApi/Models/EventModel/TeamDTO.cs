using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class TeamDTO
    {
        public int TeamOID { get; set; }
        public string Name { get; set; }
        public int SportOID { get; set; }
        public string PhotoString { get; set; }
        public byte[] Photo { get; set; }
        public bool IsFavorite { get; set; }
    }
}

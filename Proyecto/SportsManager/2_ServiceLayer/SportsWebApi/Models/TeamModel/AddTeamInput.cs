using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.TeamModel
{
    public class AddTeamInput
    {
        public string Name { get; set; }
        public int SportOID { get; set; }
    }
}

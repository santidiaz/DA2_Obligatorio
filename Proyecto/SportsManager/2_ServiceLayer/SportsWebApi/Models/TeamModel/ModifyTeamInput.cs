using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.TeamModel
{
    public class ModifyTeamInput
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}

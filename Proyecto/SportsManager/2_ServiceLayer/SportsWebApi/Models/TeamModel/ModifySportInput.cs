using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.TeamModel
{
    public class ModifySportInput
    {
        public string NewName { get; set; }
        public string OldName { get; set; }
    }
}

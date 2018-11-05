using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.TeamModel
{
    public class ModifyTeamInput
    {
        [Required(ErrorMessage = "OldName is required.")]
        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}

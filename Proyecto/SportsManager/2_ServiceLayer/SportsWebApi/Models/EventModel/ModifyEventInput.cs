using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class ModifyEventInput
    {
        [Required(ErrorMessage = "Local team required.")]
        public string LocalTeamName { get; set; }

        [Required(ErrorMessage = "Away team required.")]
        public string AwayTeamName { get; set; }

        [Required(ErrorMessage = "Initial date required.")]
        public DateTime InitialDate { get; set; }
    }
}

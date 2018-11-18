using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class ModifyEventInput
    {
        [Required(ErrorMessage = "Teams are required.")]
        public List<string> TeamsString { get; set; }

        [Required(ErrorMessage = "Initial date required.")]
        public DateTime InitialDate { get; set; }
    }
}

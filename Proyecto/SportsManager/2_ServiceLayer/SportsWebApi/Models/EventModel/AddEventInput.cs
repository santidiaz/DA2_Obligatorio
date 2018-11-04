using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class AddEventInput
    {
        [Required]        
        public string SportName { get; set; }

        [Required]
        public List<string> TeamNames { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
    }
}

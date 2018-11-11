using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class EventResultInput
    {
        [Required()]
        public List<string> TeamNames{ get; set; }        

        public bool DrawMatch { get; set; } = false;
    }
}

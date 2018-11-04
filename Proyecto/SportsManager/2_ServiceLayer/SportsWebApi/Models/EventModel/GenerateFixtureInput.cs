using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class GenerateFixtureInput
    {
        [Required]
        public string FixtureName { get; set; }

        [Required]
        public string SportName { get; set; }

        [Required]
        public DateTime InitialDate { get; set; }
    }
}

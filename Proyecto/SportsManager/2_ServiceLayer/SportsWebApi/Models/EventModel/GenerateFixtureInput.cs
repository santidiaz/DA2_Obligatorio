using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class GenerateFixtureInput
    {
        [Required(ErrorMessage = "Fixture name is required.")]
        public string FixtureName { get; set; }

        [Required]
        public string SportName { get; set; }

        [Required]
        public DateTime InitialDate { get; set; }

        [Required(ErrorMessage = "User name that execute this action is required.")]
        public string UserName { get; set; }
    }
}

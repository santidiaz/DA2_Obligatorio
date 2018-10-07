using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.TeamModel
{
    public class AddSportInput
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}

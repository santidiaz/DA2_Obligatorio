using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models
{
    public class ModifyUserInput
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(15)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(15)]
        public string Password { get; set; }

        [Required(ErrorMessage = "IsAdmin need to be specified.")]
        public bool IsAdmin { get; set; } = false;
    }
}

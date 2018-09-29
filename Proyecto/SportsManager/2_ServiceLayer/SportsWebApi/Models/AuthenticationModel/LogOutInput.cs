using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.AuthenticationModel
{
    public class LogOutInput
    {
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }
    }
}

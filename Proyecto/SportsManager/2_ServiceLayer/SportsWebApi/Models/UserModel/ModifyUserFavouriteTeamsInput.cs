using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.UserModel
{
    public class ModifyUserFavouriteTeamsInput
    {
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        public List<string> TeamNames { get; set; }
    }
}

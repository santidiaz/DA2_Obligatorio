using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.UserModel
{
    public class AddFavoritesToUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public List<int> NetFavouriteTeamsOID { get; set; }
    }
}

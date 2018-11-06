using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.AuthenticationModel
{
    public class SessionDTO
    {
        public Guid Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}

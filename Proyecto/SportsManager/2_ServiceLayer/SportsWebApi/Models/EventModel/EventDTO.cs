using BusinessEntities;
using SportsWebApi.Models.TeamModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.EventModel
{
    public class EventDTO
    {
        public int Id { get; set; }

        public List<TeamDTO> Teams { get; set; }

        public DateTime InitialDate { get; set; }

        public List<Comment> Comments { get; set; }



        public string Description { get; set; }
    }
}

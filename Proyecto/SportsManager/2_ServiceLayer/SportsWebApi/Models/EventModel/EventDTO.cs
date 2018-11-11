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
        public DateTime InitialDate { get; set; }
        public bool AllowMultipleTeams { get; set; }
        public List<Team> Teams { get; set; }
        public List<Comment> Comments { get; set; }
        public int SportId { get; set; }
        public string SportName { get; set; }
        public EventResult Result { get; set; }
    }
}

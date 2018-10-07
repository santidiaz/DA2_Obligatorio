using System.ComponentModel.DataAnnotations;

namespace SportsWebApi.Models.TeamModel
{
    public class AddTeamInput
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15)]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "SportId required.")]
        public int SportID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class TeamResult
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int TeamPoints { get; set; }

        public TeamResult(string teamName, int teamPoints)
        {
            this.TeamName = teamName;
            this.TeamPoints = teamPoints;
        }
    }
}

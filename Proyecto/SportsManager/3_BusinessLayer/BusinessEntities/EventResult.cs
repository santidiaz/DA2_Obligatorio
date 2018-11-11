using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class EventResult
    {
        private readonly bool IsDrawMatch;

        public int Id { get; set; }
        public virtual List<TeamResult> TeamsResult { get; set; }

        public EventResult() { }
        public EventResult(List<string> teamNames, bool isMultipleEventResult, bool drawMatch)
        {
            this.IsDrawMatch = drawMatch;
            this.SetResults(teamNames, isMultipleEventResult);
        }
        
        public TeamResult GetFirst()
        {
            return this.TeamsResult[0];
        }
        public TeamResult GetSecond()
        {
            return this.TeamsResult[1];
        }
        public List<TeamResult> GetEventResults()
        {
            return this.TeamsResult;
        }

        #region Private Methods
        private void SetResults(List<string> teamNames, bool isMultipleEventResult)
        {
            this.TeamsResult = new List<TeamResult>();
            if (isMultipleEventResult)
                GenerateResultForPlayers(teamNames);
            else
                GenerateResultForTeams(teamNames);
        }
        private void GenerateResultForTeams(List<string> teamNames)
        {
            this.TeamsResult = new List<TeamResult>();
            if (IsDrawMatch)
            {
                this.TeamsResult.Add(new TeamResult(teamNames[0], 1));
                this.TeamsResult.Add(new TeamResult(teamNames[1], 1));
            }
            else
            {
                this.TeamsResult.Add(new TeamResult(teamNames[0], 3));
                this.TeamsResult.Add(new TeamResult(teamNames[1], 0));
            }
        }
        private void GenerateResultForPlayers(List<string> teamsResult)
        {
            this.TeamsResult = new List<TeamResult>();
            int count = 0;
            teamsResult.ForEach(teamName
                => {
                    if (count == 0)
                        this.TeamsResult.Add(new TeamResult(teamName, 3));
                    else if (count == 1)
                        this.TeamsResult.Add(new TeamResult(teamName, 2));
                    else if (count == 3)
                        this.TeamsResult.Add(new TeamResult(teamName, 1));
                    else
                        this.TeamsResult.Add(new TeamResult(teamName, 0));
                });
        }
        #endregion
    }
}

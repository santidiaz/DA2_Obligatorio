using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class EventResult
    {
        private readonly bool IsDrawMatch;

        public int Id { get; set; }
        public IList<Tuple<string, int>> TeamsResult { get; set; }

        public EventResult(List<string> teamNames, bool isMultipleEventResult, bool drawMatch)
        {
            this.IsDrawMatch = drawMatch;
            this.SetResults(teamNames, isMultipleEventResult);
        }
        
        public Tuple<string, int> GetFirst()
        {
            return this.TeamsResult[0];
        }
        public Tuple<string, int> GetSecond()
        {
            return this.TeamsResult[1];
        }
        public IList<Tuple<string, int>> GetEventResults()
        {
            return this.TeamsResult;
        }

        #region Private Methods
        private void SetResults(List<string> teamsResult, bool isMultipleEventResult)
        {
            this.TeamsResult = new List<Tuple<string, int>>();
            if (isMultipleEventResult)
                GenerateResultForPlayers(teamsResult);
            else
                GenerateResultForTeams(teamsResult);
        }
        private void GenerateResultForTeams(List<string> teamsResult)
        {
            this.TeamsResult = new List<Tuple<string, int>>();
            if (IsDrawMatch)
            {
                this.TeamsResult.Add(Tuple.Create(teamsResult[0], 1));
                this.TeamsResult.Add(Tuple.Create(teamsResult[1], 1));
            }
            else
            {
                this.TeamsResult.Add(Tuple.Create(teamsResult[0], 3));
                this.TeamsResult.Add(Tuple.Create(teamsResult[1], 0));
            }
        }
        private void GenerateResultForPlayers(List<string> teamsResult)
        {
            this.TeamsResult = new List<Tuple<string, int>>();
            int count = 0;
            teamsResult.ForEach(teamName
                => {
                    if (count == 0)
                        this.TeamsResult.Add(Tuple.Create(teamName, 3));
                    else if (count == 1)
                        this.TeamsResult.Add(Tuple.Create(teamName, 2));
                    else if (count == 3)
                        this.TeamsResult.Add(Tuple.Create(teamName, 1));
                    else
                        this.TeamsResult.Add(Tuple.Create(teamName, 0));
                });
        }
        #endregion
    }
}

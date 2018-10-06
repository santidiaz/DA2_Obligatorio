using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using FixtureLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixtureLogic
{
    public class FinalPhaseLogic : IFixture
    {
        public List<Event> GenerateFixture(Sport aSport, DateTime initialDate)
        {
            List<Team> teams = aSport.Teams;
            if (HasEnoughTeams(teams.Count))
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            List<Match> availableMatches = this.GenerateAvailableMatches(teams);
            List<Match> shuffledListOfMatches = this.ShuffleMatches(availableMatches);
            List<Event> generatedEvents = this.GenerateEvents(shuffledListOfMatches, initialDate, aSport);

            return generatedEvents;
        }

        #region Private Methods
        private static bool HasEnoughTeams(int teamsCount)
        {
            // Number of teams has to be power of 2. (2, 4, 8, 16...)
            return !((teamsCount != 0) && ((teamsCount & (teamsCount - 1)) == 0));
        }

        private bool DoesTeamMatchExists(List<Match> matches, Team teamA, Team teamB)
        {
            return !matches.Exists(m => m.Away.Equals(teamA) ||
                                        m.Local.Equals(teamA) ||
                                        m.Away.Equals(teamB) ||
                                        m.Local.Equals(teamB));
        }

        private List<Match> GenerateAvailableMatches(List<Team> teams)
        {
            List<Match> generatedMatches = new List<Match>();
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = 0; j < teams.Count; j++)
                {
                    if (teams[i] != teams[j] &&
                        this.DoesTeamMatchExists(generatedMatches, teams[i], teams[j]))
                    {
                        generatedMatches.Add(
                            new Match
                            {
                                IsAvailable = true,
                                Local = teams[i],
                                Away = teams[j]
                            });
                    }
                }
            }

            return generatedMatches;
        }

        private List<Match> ShuffleMatches(List<Match> inputList)
        {
            List<Match> randomList = new List<Match>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        private List<Event> GenerateEvents(List<Match> matches, DateTime initialDate, Sport aSport)
        {
            List<Event> generatedEvents = new List<Event>();
            foreach (Match currentMatch in matches)
            {
                generatedEvents.Add(new Event(initialDate, aSport, currentMatch.Local, currentMatch.Away));
                initialDate = initialDate.AddDays(1);
            }
            return generatedEvents;
        }
        #endregion
    }
}

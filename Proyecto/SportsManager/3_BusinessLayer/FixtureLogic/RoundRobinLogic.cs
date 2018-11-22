using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using FixtureContracts;
using FixtureLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureLogic
{
    public class RoundRobinLogic : IFixture
    {
        public List<Event> GenerateFixture(Sport aSport, DateTime initialDate)
        {
            List<Team> sportTeams = aSport.Teams;
            if (this.HasEnoughTeams(sportTeams.Count))
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            int gamesPerDay = (sportTeams.Count / 2);
            List<Match> availableMatches = this.GenerateAvailableMatches(sportTeams);            
            List<Event> events = this.GenerateEvents(aSport, availableMatches, initialDate, gamesPerDay);

            return events;
        }

        public string GetDescription()
        {
            return "Round Robin";
        }

        #region Private Methods
        private bool HasEnoughTeams(int teamsCount)
        {

            /*
             if(admite multiple encuentro)
                teamsCount > 2
             
             
             
             */

            // si el Sport.AdmiteMultipleEncuentro
            // && teamsCount 


            return (teamsCount % 2) != 0;
        }

        private List<Match> GenerateAvailableMatches(List<Team> teams)
        {
            List<Match> matches = new List<Match>();
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = 0; j < teams.Count; j++)
                {
                    if (teams[i] != teams[j])
                    {
                        matches.Add(
                            new Match
                            {
                                IsAvailable = true,
                                Local = teams[i],
                                Away = teams[j]
                            });
                    }
                }
            }
            return matches;
        }

        private List<Event> GenerateEvents(Sport aSport, List<Match> availableMatches, DateTime initialDate, int gamesPerDay)
        {
            List<Event> events = new List<Event>();
            int addedGames = 1; // First iteration is alredy counted.
            do
            {
                foreach (Match aMatch in availableMatches)
                {
                    if (aMatch.IsAvailable)
                    {
                        if (events.Count != 0)
                        {
                            if (addedGames.Equals(gamesPerDay))
                            { // If max events per day is reached, update date.
                                initialDate = initialDate.AddDays(1);
                                addedGames = 1;
                            }
                            else
                            {
                                if (this.DoesTodayTeamMatchExists(events, aMatch, initialDate))
                                {
                                    //events.Add(new Event(initialDate, aSport, aMatch.Local, aMatch.Away));
                                    List<Team> teams = new List<Team>();
                                    teams.Add(aMatch.Local);
                                    teams.Add(aMatch.Away);
                                    events.Add(new Event(initialDate, aSport, teams));
                                    aMatch.IsAvailable = false;
                                    addedGames++;
                                }
                            }
                        }
                        else
                        {   // First iteration
                            
                            //events.Add(new Event(initialDate, aSport, aMatch.Local, aMatch.Away));
                            List<Team> teams = new List<Team>();
                            teams.Add(aMatch.Local);
                            teams.Add(aMatch.Away);
                            events.Add(new Event(initialDate, aSport, teams));
                            aMatch.IsAvailable = false;
                        }
                    }
                }
            } while (this.ExistsAvailableMatches(availableMatches));

            return events;
        }

        private bool DoesTodayTeamMatchExists(List<Event> events, Match aMatch, DateTime initialDate)
        {
            return !events.Exists(ev => ev.InitialDate.Date.Equals(initialDate.Date) &&
                                        (ev.GetLocalTeam().Equals(aMatch.Local) ||
                                        ev.GetAwayTeam().Equals(aMatch.Local) ||
                                        ev.GetLocalTeam().Equals(aMatch.Away) ||
                                        ev.GetAwayTeam().Equals(aMatch.Away)));
        }

        private bool ExistsAvailableMatches(List<Match> availableMatches)
        {
            return !availableMatches.TrueForAll(am => !am.IsAvailable);
        }
        #endregion
    }
}

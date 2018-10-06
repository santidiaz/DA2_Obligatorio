using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
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
            if ((sportTeams.Count % 2) != 0)
                throw new EntitiesException(Constants.SportErrors.NOT_ENOUGH_TEAMS, ExceptionStatusCode.InvalidData);

            var availableMatches = new List<Match>();
            for (int i = 0; i < sportTeams.Count; i++)
            {
                for (int j = 0; j < sportTeams.Count; j++)
                {
                    if (sportTeams[i] != sportTeams[j])
                    {
                        availableMatches.Add(
                            new Match
                            {
                                IsAvailable = true,
                                Local = sportTeams[i],
                                Away = sportTeams[j]
                            });
                    }
                }
            }

            int gamesPerDay = (sportTeams.Count / 2);
            int addedGames = 1; // First iteration is alredy counted.
            List<Event> events = new List<Event>();
            do
            {
                foreach (Match aMatch in availableMatches)
                {
                    if (aMatch.IsAvailable)
                    {
                        if (events.Count != 0)
                        {
                            if (addedGames.Equals(gamesPerDay))
                            { // Si alcanze el maximo posible de encuentros por dia (segun la cantidad de equipos que tengo).. aumento la fecha de inicio
                                initialDate = initialDate.AddDays(1);
                                addedGames = 1;
                            }
                            else
                            {
                                // Si para la FECHA DE HOY, existe alguno de los equipos del MATCH en iteracion, lo skipeo.
                                if (!events.Exists(ev =>
                                             ev.InitialDate.Date.Equals(initialDate.Date) &&
                                             (ev.GetFirstTeam().Equals(aMatch.Local) ||
                                              ev.GetSecondTeam().Equals(aMatch.Local) ||
                                              ev.GetFirstTeam().Equals(aMatch.Away) || 
                                              ev.GetSecondTeam().Equals(aMatch.Away))
                                              ))
                                {
                                    events.Add(new Event(initialDate, aSport, aMatch.Local, aMatch.Away));
                                    aMatch.IsAvailable = false;
                                    addedGames++;
                                }
                            }                            
                        }
                        else
                        {// First iteration
                            events.Add(new Event(initialDate, aSport, aMatch.Local, aMatch.Away));
                            aMatch.IsAvailable = false;
                        }
                    }
                }
            } while (!availableMatches.TrueForAll(am => !am.IsAvailable));

            return events;
        }
    }

    internal class Match
    {
        public Team Local { get; set; }
        public Team Away { get; set; }
        public bool IsAvailable { get; set; }
    }
}

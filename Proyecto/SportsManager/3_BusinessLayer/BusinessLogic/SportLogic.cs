using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class SportLogic : ISportLogic
    {
        private ISportPersistance persistanceProvider;

        public SportLogic(ISportPersistance provider)
        {
            this.persistanceProvider = provider;
        }

        #region Public methods
        public void AddSport(Sport sportToAdd)
        {
            if (this.IsSportInSystem(sportToAdd))
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS, ExceptionStatusCode.InvalidData);

            this.persistanceProvider.AddSport(sportToAdd);
        }

        public bool IsSportInSystem(Sport sport)
        {
            bool result = false;
            List<Sport> systemSports = this.persistanceProvider.GetSports();
            foreach (var sportAux in systemSports)
                if (sportAux.Name == sport.Name) result = true;

            return result;
        }

        public void ModifySportByName(string nameSport, Sport sport)
        {
            Sport sportToModify = this.GetSportByName(nameSport);

            sport.Id = sportToModify.Id;
            this.persistanceProvider.ModifySport(sport);
        }

        public Sport GetSportByName(string name)
        {
            var sport = this.persistanceProvider.GetSportByName(name, true);
            if (sport == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);

            return sport;
        }

        public void DeleteSportByName(string name)
        {
            Sport sportToDelete = this.GetSportByName(name);
            
            if (!this.CanBeDeleted(sportToDelete))
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_HAS_TEAMS, ExceptionStatusCode.Conflict);

            this.persistanceProvider.DeleteSport(sportToDelete);
        }

        public List<Event> GetEventsBySport(string sportName)
        {
            List<Event> events = new List<Event>();
            Sport sport = this.GetSportByName(sportName);

            return this.persistanceProvider.GetEventsBySport(sport);
        }

        public List<Sport> GetSports()
        {
            return this.persistanceProvider.GetSports();
        }
        public Dictionary<string, int> GetSportResultTable(string sportName)
        {
            var result = new Dictionary<string, int>();
            List<Event> events = GetEventsBySport(sportName);

            events = events.FindAll(e => e.HasResult());
            events.ForEach(
                ev =>
                {
                    ev.Result.TeamsResult.ForEach(
                            tr =>
                            {
                                if (result.ContainsKey(tr.TeamName))
                                    result[tr.TeamName] += tr.TeamPoints;
                                else
                                    result.Add(tr.TeamName, tr.TeamPoints);
                            });
                });

            return result.OrderBy(dic => dic.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        #endregion

        #region Private Methods
        private bool CanBeDeleted(Sport sport)
        {
            return persistanceProvider.CanBeDeleted(sport);
        }
        #endregion
    }
}

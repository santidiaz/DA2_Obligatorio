using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
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
            if (sportToDelete == null)
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_DO_NOT_EXISTS, ExceptionStatusCode.NotFound);

            if (this.CanBeDeleted(sportToDelete))
                throw new EntitiesException(Constants.SportErrors.ERROR_SPORT_HAS_TEAMS, ExceptionStatusCode.Conflict);

            this.persistanceProvider.DeleteSport(sportToDelete);
        }

        public List<Event> GetEventsBySport(string sportName)
        {
            List<Event> events = new List<Event>();
            Sport sport = this.GetSportByName(sportName);

            return this.persistanceProvider.GetEventsBySport(sport);
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

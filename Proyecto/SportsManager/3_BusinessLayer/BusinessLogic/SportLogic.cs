using BusinessEntities;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class SportLogic : BusinessContracts.ISportLogic
    {
        private ISportPersistance persistanceProvider;

        public SportLogic(ISportPersistance provider)
        {
            this.persistanceProvider = provider;
        }

        public void AddSport(Sport sportToAdd)
        {
            if (this.IsSportInSystem(sportToAdd))
                throw new Exception(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS);
            else
                this.persistanceProvider.AddSport(sportToAdd);
        }

        private bool IsSportInSystem(Sport sport)
        {
            bool result = false;
            List<Sport> systemSports = this.persistanceProvider.GetSports();
            foreach (var sportAux in systemSports)
            {
                if (sportAux.Equals(sport)) { result = true; };

            }
            return result;
        }

        public void ModifySportByName(string nameSport, Sport sport)
        {
            Sport sportToModify = this.GetSportByName(nameSport);
            if (sportToModify == null)
                throw new Exception(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS);
            else
                this.persistanceProvider.ModifySportByName(nameSport, sport);
        }

        public Sport GetSportByName(string name)
        {
            var sport = this.persistanceProvider.GetSportByName(name);

            if (sport == null)
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS);
            return sport;
        }
    }
}

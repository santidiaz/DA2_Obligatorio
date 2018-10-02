using BusinessContracts;
using BusinessEntities;
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

        public void AddSport(Sport sportToAdd)
        {
            if (this.IsSportInSystem(sportToAdd))
                throw new Exception(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS);

            this.persistanceProvider.AddSport(sportToAdd);
        }

        public bool IsSportInSystem(Sport sport)
        {
            bool result = false;
            List<Sport> systemSports = this.persistanceProvider.GetSports();
            foreach (var sportAux in systemSports)
                if (sportAux.Equals(sport)) result = true;

            return result;
        }

        public void ModifySportByName(string nameSport, Sport sport)
        {
            Sport sportToModify = this.GetSportByName(nameSport);
            
            if (sportToModify == null)
                throw new Exception(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS);
            else
            {
                sport.SportOID = sportToModify.SportOID;
                this.persistanceProvider.ModifySportByName(sport);
            }
        }

        public Sport GetSportByName(string name)
        {
            var sport = this.persistanceProvider.GetSportByName(name);
            if (sport == null)
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS);

            return sport;
        }

        public bool DeleteSportByName(string name)
        {
            try
            {
                bool result = true;
                Sport sportToDelete = this.GetSportByName(name);

                if (sportToDelete != null)
                    this.persistanceProvider.DeleteSportByName(sportToDelete);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS, ex);
            }
        }

        public List<Event> GetEventsBySport(string sportName)
        {
            try
            {
                List<Event> events = new List<Event>();
                Sport sport = this.GetSportByName(sportName);

                if (sport != null)
                    return this.persistanceProvider.GetEventsBySport(sport);
                else
                    return events;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS, ex);
            }
        }
    }
}

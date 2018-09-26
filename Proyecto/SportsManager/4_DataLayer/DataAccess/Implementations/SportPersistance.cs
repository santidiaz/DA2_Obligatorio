using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Implementations
{
    public class SportPersistance : ISportPersistance
    {
        public void AddSport(Sport newSport)
        {
            throw new NotImplementedException();
        }

        public void DeleteSportByName(string name)
        {
            throw new NotImplementedException();
        }

        public Sport GetSportByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Sport> GetSports()
        {
            throw new NotImplementedException();
        }

        public bool IsSportInSystem(Sport Sport)
        {
            throw new NotImplementedException();
        }

        public void ModifySportByName(string name, Sport newSport)
        {
            throw new NotImplementedException();
        }
    }
}

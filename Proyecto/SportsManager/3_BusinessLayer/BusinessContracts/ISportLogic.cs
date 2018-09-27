using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ISportLogic
    {
        void AddSport(Sport sportToAdd);
        bool IsSportInSystem(Sport sport);
        void ModifySportByName(string nameSport, Sport sport);
        Sport GetSportByName(string name);
        bool DeleteSportByName(string userToBeDeleted);
    }
}

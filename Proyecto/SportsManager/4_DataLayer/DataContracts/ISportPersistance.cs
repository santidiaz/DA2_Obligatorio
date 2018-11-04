using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ISportPersistance
    {
        void AddSport(Sport newSport);
        void DeleteSport(Sport sportToDelete);
        void ModifySport(Sport sportToModify);
        List<Sport> GetSports();
        Sport GetSportByName(string name, bool eageLoad = false);
        Sport GetSportById(int id, bool eagerLoad = false);
        List<Event> GetEventsBySport(Sport sport);

        bool IsSportInSystem(Sport Sport);            
        bool CanBeDeleted(Sport sport);
    }
}

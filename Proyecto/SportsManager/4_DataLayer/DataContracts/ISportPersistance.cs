using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace DataContracts
{
    public interface ISportPersistance
    {
        void AddSport(Sport newSport);
        List<Sport> GetSports();
        void ModifySportByName(Sport newSport);
        void DeleteSportByName(Sport name);
        bool IsSportInSystem(Sport Sport);
        List<Event> GetEventsBySport(Sport sport);
        Sport GetSportByName(string name, bool eageLoad = false);
        bool ValidateSportOnTeams(Sport sport);
    }
}

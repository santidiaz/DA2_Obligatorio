﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace DataContracts
{
    public interface ISportPersistance
    {
        void AddSport(Sport newSport);
        List<Sport> GetSports();
        void ModifySportByName(string name, Sport newSport);
        Sport GetSportByName(string name);
        void DeleteSportByName(string name);
        bool IsSportInSystem(Sport Sport);
    }
}

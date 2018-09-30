using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContracts
{
    public interface ISportPersistance
    {
        Sport GetSportByName(string name, bool eageLoad);
    }
}

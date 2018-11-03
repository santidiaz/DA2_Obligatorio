using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureContracts
{
    public interface IFixture
    {
        List<Event> GenerateFixture(Sport aSport, DateTime initialDate);
    }
}

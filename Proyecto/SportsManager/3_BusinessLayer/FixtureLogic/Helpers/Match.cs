using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureLogic.Helpers
{
    internal class Match
    {
        public Team Local { get; set; }
        public Team Away { get; set; }
        public bool IsAvailable { get; set; }
    }
}

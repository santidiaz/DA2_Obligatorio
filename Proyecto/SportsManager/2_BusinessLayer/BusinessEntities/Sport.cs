using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Sport
    {
        public int SportOID { get; set; } // [Object Id] This id is team by EntityFramework.
        public string Name { get; set; }
        public List<Team> TeamsList { get; set; } //ver como guardar foto.
    }
}

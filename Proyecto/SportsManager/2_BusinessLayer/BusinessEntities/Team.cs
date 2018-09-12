using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Team
    {
        public int TeamOID { get; set; } // [Object Id] This id is team by EntityFramework.
        public string Name { get; set; }
        public string Photo { get; set; } //ver como guardar foto.
    }
}

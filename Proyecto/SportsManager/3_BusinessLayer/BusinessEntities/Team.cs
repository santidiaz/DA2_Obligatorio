using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Team
    {
        public Team()
        {

        }
        public Team(string expectedName, byte[] expectedPhoto)
        {
            this.Name = expectedName;
            this.Photo = expectedPhoto;
        }

        public int TeamOID { get; set; } // [Object Id] This id is team by EntityFramework.
        public string Name { get; set; }
        public byte[] Photo { get; set; } //ver como guardar foto.

        public override bool Equals(object obj)
        {
            if (obj is Team)
                return this.Name.Equals(((Team)obj).Name);
            else
                return false;
        }
    }
}

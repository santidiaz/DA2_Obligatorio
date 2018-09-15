using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class SportLogic : BusinessContracts.ISportLogic
    {
        private ISportPersistance PersistanceProvider;

        public SportLogic(ISportPersistance provider)
        {
            this.PersistanceProvider = provider;
        }
    }
}

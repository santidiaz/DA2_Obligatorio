using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class TeamLogic : BusinessContracts.ITeamLogic
    {
        private ITeamPersistance PersistanceProvider;

        public TeamLogic(ITeamPersistance provider)
        {
            this.PersistanceProvider = provider;
        }
    }
}

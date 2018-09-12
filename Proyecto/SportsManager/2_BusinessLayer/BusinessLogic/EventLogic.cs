using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class EventLogic : BusinessContracts.IEventLogic
    {
        private IEventPersistance PersistanceProvider;

        public EventLogic(IEventPersistance provider)
        {
            this.PersistanceProvider = provider;
        }
    }
}

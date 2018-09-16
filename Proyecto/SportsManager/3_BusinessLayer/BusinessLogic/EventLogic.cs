using BusinessContracts;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class EventLogic : IEventLogic
    {
        private IEventPersistance PersistanceProvider;

        public EventLogic(IEventPersistance provider)
        {
            this.PersistanceProvider = provider;
        }
    }
}
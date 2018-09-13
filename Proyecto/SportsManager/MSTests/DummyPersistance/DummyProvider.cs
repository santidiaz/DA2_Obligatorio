using BusinessContracts;
using BusinessLogic;
using DummyPersistance.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyPersistance
{
    public class DummyProvider
    {
        private IUserLogic UserLogic;
        private ITeamLogic TeamLogic;
        private ISportLogic SportLogic;
        private IEventLogic EventLogic;

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<DummyProvider> currentInstance = new Lazy<DummyProvider>(() => new DummyProvider());
        private DummyProvider()
        {
            this.UserLogic = new UserLogic(new UserDummyPersistance());
            this.TeamLogic = new TeamLogic(new TeamDummyPersistance());
            this.SportLogic = new SportLogic(new SportDummyPersistance());
            this.EventLogic = new EventLogic(new EventDummyPersistance());
        }
        public static DummyProvider GetInstance
        {
            get
            {
                return currentInstance.Value;
            }
        }
        #endregion

        public IUserLogic GetUserOperations()
        {
            return this.UserLogic;
        }

        public ITeamLogic GetTeamOperations()
        {
            return this.TeamLogic;
        }

        public ISportLogic GetSportOperations()
        {
            return this.SportLogic;
        }

        public IEventLogic GetEventOperations()
        {
            return this.EventLogic;
        }
    }
}

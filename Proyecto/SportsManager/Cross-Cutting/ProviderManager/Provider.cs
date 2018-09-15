using BusinessContracts;
using BusinessLogic;
using DataAccess.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProviderManager
{
    public class Provider
    {
        private IUserLogic userLogic;
        private ITeamLogic teamLogic;
        private ISportLogic sportLogic;
        private IEventLogic eventLogic;

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<Provider> currentInstance = new Lazy<Provider>(() => new Provider());
        private Provider()
        {
            this.userLogic = new UserLogic(new UserPersistance());
            this.teamLogic = new TeamLogic(new TeamPersistance());
            this.sportLogic = new SportLogic(new SportPersistance());
            this.eventLogic = new EventLogic(new EventPersistance());
        }
        public static Provider GetInstance
        {
            get
            {
                return currentInstance.Value;
            }
        }
        #endregion

        public IUserLogic GetUserOperations()
        {
            return this.userLogic;
        }

        public ITeamLogic GetTeamOperations()
        {
            return this.teamLogic;
        }

        public ISportLogic GetSportOperations()
        {
            return this.sportLogic;
        }

        public IEventLogic GetEventOperations()
        {
            return this.eventLogic;
        }
    }
}

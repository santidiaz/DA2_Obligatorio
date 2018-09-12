using BusinessContracts;
using BusinessLogic;
using DataAccess.Implementations;
using System;

namespace ProviderManager
{
    public class Provider
    {
        private IUserLogic UserLogic;
        private ITeamLogic TeamLogic;
        private ISportLogic SportLogic;

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<Provider> currentInstance = new Lazy<Provider>(() => new Provider());
        private Provider()
        {
            this.UserLogic = new UserLogic(new UserPersistance());
            this.TeamLogic = new TeamLogic(new TeamPersistance());
            this.SportLogic = new SportLogic(new SportPersistance());
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
    }
}

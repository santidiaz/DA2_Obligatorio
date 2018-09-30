using BusinessContracts;
using BusinessLogic;
using DataAccess.Implementations;
using DataContracts;
using ProviderManager.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProviderManager
{
    public class Provider
    {
        #region Private properties
        private IUserLogic userLogic;
        private ITeamLogic teamLogic;
        private ISportLogic sportLogic;
        private IEventLogic eventLogic;
        private IFixture fixture;

        private IUserPersistance userPersistance;
        private ITeamPersistance teamPersistance;
        private ISportPersistance sportPersistance;
        private IEventPersistance eventPersistance;
        #endregion

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<Provider> currentInstance = new Lazy<Provider>(() => new Provider());
        private Provider()
        {
            this.Initialize();
        }
        private void Initialize()
        {
            this.CreatePersistances();
            this.CreateLogics();
        }
        private void CreatePersistances()
        {
            this.userPersistance = new UserPersistance();
            this.teamPersistance = new TeamPersistance();
            this.sportPersistance = new SportPersistance();
            this.eventPersistance = new EventPersistance();
        }
        private void CreateLogics()
        {
            this.userLogic = new UserLogic(userPersistance, teamPersistance);
            this.teamLogic = new TeamLogic(teamPersistance);
            this.sportLogic = new SportLogic(sportPersistance);
            this.eventLogic = new EventLogic(eventPersistance);
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
        public IFixture GetFixtureGenerator(FixtureType fixtureType)
        {
            IFixture fixtureGenerationAlgorithm;
            switch (fixtureType)
            {
                case FixtureType.Groups:
                    fixtureGenerationAlgorithm = new GroupFixture();
                    break;
                case FixtureType.RoundTrip:
                    fixtureGenerationAlgorithm = null;
                    break;
                default:
                    fixtureGenerationAlgorithm = null;
                    break;
            }
            return fixtureGenerationAlgorithm;
        }
    }
}
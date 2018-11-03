using BusinessLogic;
using DataAccess.Implementations;
using DataContracts;
using ProviderManager.Helpers;
using PermissionContracts;
using PermissionLogic;
using System;
using System.Collections.Generic;
using System.Text;
using FixtureLogic;
using FixtureContracts;
using BusinessContracts;

namespace ProviderManager
{
    public class Provider
    {
        #region Private properties
        private IUserLogic userLogic;
        private ITeamLogic teamLogic;
        private ISportLogic sportLogic;
        private IEventLogic eventLogic;
        private IPermissionLogic permissionLogic;
		private ICommentLogic commentLogic;

        private IUserPersistance userPersistance;
        private ITeamPersistance teamPersistance;
        private ISportPersistance sportPersistance;
        private IEventPersistance eventPersistance;
        private IPermissionPersistance permissionPersistance;
        private ICommentPersistance commentPersistance;
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
            this.permissionPersistance = new PermissionPersistance();
            this.commentPersistance = new CommentPersistance();
        }
        private void CreateLogics()
        {
            this.userLogic = new UserLogic(userPersistance, teamPersistance);
            this.teamLogic = new TeamLogic(teamPersistance, sportPersistance);
            this.sportLogic = new SportLogic(sportPersistance);
            this.eventLogic = new EventLogic(eventPersistance, sportPersistance, teamPersistance);
			this.commentLogic = new CommentLogic(commentPersistance, eventPersistance);
            this.permissionLogic = new PermissionLogic.PermissionLogic(permissionPersistance, userPersistance);
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

        public IPermissionLogic GetPermissionOperations()
        {
            return this.permissionLogic;
        }
		
		public ICommentLogic GetCommentOperations()
        {
            return this.commentLogic;
        }

        public IFixture GetFixtureGenerator(FixtureType fixtureType)
        {
            IFixture fixtureGenerationAlgorithm;
            switch (fixtureType)
            {
                case FixtureType.FinalPhase:
                    fixtureGenerationAlgorithm = new FinalPhaseLogic();
                    break;
                case FixtureType.RoundTrip:
                    fixtureGenerationAlgorithm = new RoundRobinLogic();
                    break;
                default:
                    fixtureGenerationAlgorithm = null;
                    break;
            }
            return fixtureGenerationAlgorithm;
        }
    }
}
using FixtureContracts;
using FixtureLogic;
using System;
using System.Collections.Generic;

namespace FixtureProvider
{
    public class Provider
    {
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
            //this.userPersistance = new UserPersistance();
        }
        private void CreateLogics()
        {
            //this.userLogic = new UserLogic(userPersistance, teamPersistance);
        }
        public static Provider GetInstance
        {
            get
            {
                return currentInstance.Value;
            }
        }

        public IList<IFixture> GetFixturesAlgorithms()
        {
            IList<IFixture> fixturesAlgorithms = new List<IFixture>
            {
                new FinalPhaseLogic(),
                new RoundRobinLogic()
            };

            // Call Reflection logic and get new algorithms


            return fixturesAlgorithms;
        }
    }
}

using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessLogic;
using CommonUtilities;
using FixtureLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class FixtureLogicTests
    {
        [TestMethod]
        public void TryGenerateRoundRobinWithNotEvenNumberOfTeams()
        {
            try
            {
                #region Initialize
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                List<Team> teams = new List<Team> { team1, team2, team3 };
                Sport sport = new Sport("Football", teams);
                #endregion

                RoundRobinLogic eventLogic = new RoundRobinLogic();
                eventLogic.GenerateFixture(sport, DateTime.Now);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.SportErrors.NOT_ENOUGH_TEAMS));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void RoundRobinTest()
        {
            try
            {
                #region Initialize
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                Team team4 = new Team { Name = "Sevilla" };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);
                #endregion

                RoundRobinLogic eventLogic = new RoundRobinLogic();
                List<Event> generatedEvents = eventLogic.GenerateFixture(sport, DateTime.Now);

                Assert.IsNotNull(generatedEvents);
                Assert.AreEqual(generatedEvents.Count, 12); // This should return 12 events.
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryGenerateFinalPhaseWithoutEnoughTeams()
        {
            try
            {
                #region Initialize
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                List<Team> teams = new List<Team> { team1, team2, team3 };
                Sport sport = new Sport("Football", teams);
                #endregion

                FinalPhaseLogic fixtureLogic = new FinalPhaseLogic();
                fixtureLogic.GenerateFixture(sport, DateTime.Now);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.SportErrors.NOT_ENOUGH_TEAMS));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
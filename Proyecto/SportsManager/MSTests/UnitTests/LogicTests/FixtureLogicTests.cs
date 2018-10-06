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
        public void TryGenerateFixtureWithNotEvenNumberOfTeams()
        {
            try
            {
                #region Initialize
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                // Team team4 = new Team { Name = "Sevilla" };
                List<Team> teams = new List<Team> { team1, team2, team3 };
                Sport sport = new Sport("Football", teams);
                //Event event1 = new Event(DateTime.Now, sport, team1, team3);
                //Event event2 = new Event(DateTime.Now.AddHours(3), sport, team2, team1);
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
                List<Team> teams = new List<Team> { team1, team2, team3 };
                Sport sport = new Sport("Football", teams);
                //Event event1 = new Event(DateTime.Now, sport, team1, team3);
                //Event event2 = new Event(DateTime.Now.AddHours(3), sport, team2, team1);
                #endregion

                RoundRobinLogic eventLogic = new RoundRobinLogic();
                List<Event> generatedEvents = eventLogic.GenerateFixture(sport, DateTime.Now);

                Assert.IsNotNull(generatedEvents);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
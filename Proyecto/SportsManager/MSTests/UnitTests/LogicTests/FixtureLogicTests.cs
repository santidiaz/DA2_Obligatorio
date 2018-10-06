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
        public void RoundRobinTest()
        {
            //// Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
            //var mockUserPersitance = new Mock<IUserPersistance>();
            //var mockTeamPersitance = new Mock<ITeamPersistance>();
            //var mockUserName = "santidiaz";
            //mockUserPersitance.Setup(up => up.DoesUserExists(mockUserName)).Returns(true);

            //// Instancio UserLogic con el mock como parametro.
            //UserLogic userLogic = new FixtureLogi(mockUserPersitance.Object, mockTeamPersitance.Object);

            //string userNameThatExists = "santidiaz";
            //string userNameThatDoNotExists = "abcdef";
            //bool resultExists = userLogic.DoesUserExists(userNameThatExists);
            //bool resultNotExists = userLogic.DoesUserExists(userNameThatDoNotExists);

            //Assert.AreEqual(true, resultExists);
            //Assert.AreEqual(false, resultNotExists);
        }

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
    }
}
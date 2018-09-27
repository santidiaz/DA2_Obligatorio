using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessLogic;
using CommonUtilities;
using DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Utilities;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class EventLogicTest
    {
        [TestMethod]
        public void TryAddEventThatExists()
        {
            try
            {
                #region Initialize Mock
                var mock = new Mock<IEventPersistance>();

                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                Team team4 = new Team { Name = "Sevilla" };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);
                Event event1 = new Event(DateTime.Now, sport, team1, team3);
                Event event2 = new Event(DateTime.Now.AddHours(3), sport, team2, team1);

                // Events that will be returned for today.
                List<Event> todaysMockedEvents = new List<Event> { event1, event2 };

                mock.Setup(up => up.GetTodayEvents()).Returns(todaysMockedEvents);
                #endregion

                // Instancio UserLogic con el mock como parametro.
                EventLogic eventLogic = new EventLogic(mock.Object);
                Event eventToValidate = new Event(DateTime.Now, sport, team1, team3);

                eventLogic.AddEvent(eventToValidate);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.EventError.ALREADY_EXISTS));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryAddEventThatDoNotExists()
        {
            try
            {
                #region Initialize Mock
                var mock = new Mock<IEventPersistance>();

                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "Defensor" };
                Team team3 = new Team { Name = "FC Barcelona" };
                Team team4 = new Team { Name = "Sevilla" };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);
                Event event1 = new Event(DateTime.Now, sport, team1, team3);
                Event event2 = new Event(DateTime.Now.AddHours(3), sport, team2, team1);

                // Todays event that will be returned.
                List<Event> todaysMockedEvents = new List<Event> { event1, event2 };

                mock.Setup(up => up.GetTodayEvents()).Returns(todaysMockedEvents);
                #endregion

                // Instancio UserLogic con el mock como parametro.
                EventLogic eventLogic = new EventLogic(mock.Object);
                Event eventToValidate = new Event(DateTime.Now, sport, team1, team2);

                eventLogic.AddEvent(eventToValidate);

                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

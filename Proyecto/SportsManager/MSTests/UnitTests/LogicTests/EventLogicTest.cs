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
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

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
                eventMock.Setup(em => em.GetEventsByDate(event1.InitialDate)).Returns(todaysMockedEvents);
                sportMock.Setup(sm => sm.GetSportByName(sport.Name, true)).Returns(sport);
                teamMock.Setup(tm => tm.GetTeamByName(team1.Name)).Returns(team1);
                teamMock.Setup(tm => tm.GetTeamByName(team3.Name)).Returns(team2);
                #endregion

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                eventLogic.AddEvent(sport.Name, team1.Name, team3.Name, event1.InitialDate);

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
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

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

                eventMock.Setup(em => em.GetEventsByDate(event1.InitialDate)).Returns(todaysMockedEvents);
                sportMock.Setup(sm => sm.GetSportByName(sport.Name, true)).Returns(sport);
                teamMock.Setup(tm => tm.GetTeamByName(team1.Name)).Returns(team1);
                teamMock.Setup(tm => tm.GetTeamByName(team2.Name)).Returns(team2);
                #endregion

                // Instancio UserLogic con el mock como parametro.
                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                eventLogic.AddEvent(sport.Name, team1.Name, team2.Name, event1.InitialDate);

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

        [TestMethod]
        public void TryGetEventByIdThatDoNotExists()
        {
            try
            {
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();
                eventMock.Setup(ep => ep.GetEventById(It.IsAny<int>(), false)).Returns((Event)null);

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                int eventToSearch = 3;
                Event foundEvent = eventLogic.GetEventById(eventToSearch);

                Assert.IsTrue(foundEvent == null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteEventById()
        {
            //try
            //{
            //    #region Initialize Mock
            //    var mock = new Mock<IEventPersistance>();

            //    Team team1 = new Team { Name = "Nacional" };
            //    Team team2 = new Team { Name = "Defensor" };
            //    Team team3 = new Team { Name = "FC Barcelona" };
            //    Team team4 = new Team { Name = "Sevilla" };
            //    List<Team> teams = new List<Team> { team1, team2, team3, team4 };
            //    Sport sport = new Sport("Football", teams);
            //    Event event1 = new Event(DateTime.Now, sport, team1, team3);
            //    Event event2 = new Event(DateTime.Now.AddHours(3), sport, team2, team1);

            //    // Todays event that will be returned.
            //    List<Event> todaysMockedEvents = new List<Event> { event1, event2 };

            //    eventMock.Setup(up => up.GetTodayEvents()).Returns(todaysMockedEvents);
            //    #endregion

            //    // Instancio UserLogic con el mock como parametro.
            //    EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
            //    Event eventToValidate = new Event(DateTime.Now, sport, team1, team2);

            //    eventLogic.AddEvent(eventToValidate);

            //    Assert.IsTrue(true);
            //}
            //catch (Exception ex)
            //{
            //    Assert.Fail(ex.Message);
            //}
        }

        [TestMethod]
        public void GetAllEvents()
        {
            try
            {
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();
                eventMock.Setup(ep => ep.GetAllEvents()).Returns(new List<Event>());

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                //int eventToSearch = 3;
                List<Event> foundEvent = eventLogic.GetAllEvents();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryToModifyEventThatDoNotExists()
        {
            try
            {
                #region Initialize Mock
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

                eventMock.Setup(em => em.GetEventById(It.IsAny<int>(), true)).Returns((Event)null);
                #endregion

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                string dummySportNameA = string.Empty;
                string dummySportNameB = string.Empty;

                eventLogic.ModifyEvent(1, dummySportNameA, dummySportNameB, DateTime.Now);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.AreEqual(eEx.Message, Constants.EventError.NOT_FOUND);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

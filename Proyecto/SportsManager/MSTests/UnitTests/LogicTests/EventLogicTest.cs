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
                var ev1Teams = new List<Team> { team1, team3 };
                var ev2Teams = new List<Team> { team2, team1 };

                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                Event event2 = new Event(DateTime.Now.AddHours(3), sport, ev2Teams);

                // Events that will be returned for today.
                List<Event> todaysMockedEvents = new List<Event> { event1, event2 };
                eventMock.Setup(em => em.GetEventsByDate(event1.InitialDate)).Returns(todaysMockedEvents);
                sportMock.Setup(sm => sm.GetSportByName(sport.Name, true)).Returns(sport);
                teamMock.Setup(tm => tm.GetTeamByName(team1.Name)).Returns(team1);
                teamMock.Setup(tm => tm.GetTeamByName(team3.Name)).Returns(team2);
                #endregion

                var expectedTeamNames = new List<string> { team1.Name, team3.Name };
                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                eventLogic.AddEvent(sport.Name, expectedTeamNames, event1.InitialDate);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                string expectedEx = string.Format(Constants.EventError.EVENT_TEAM_EXISTS, 0, DateTime.Now.Date);
                Assert.IsTrue(eEx.Message.Equals(expectedEx));
                Assert.AreEqual(eEx.StatusCode, ExceptionStatusCode.InvalidData);
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

                Team team1 = new Team { Name = "Nacional", Id = 1 };
                Team team2 = new Team { Name = "Defensor", Id = 2 };
                Team team3 = new Team { Name = "Barcelona", Id = 3 };
                Team team4 = new Team { Name = "Sevilla", Id = 4 };

                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);

                var ev1Teams = new List<Team> { team1, team3 };
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                event1.Id = 1;

                // Todays event that will be returned.
                List<Event> todaysMockedEvents = new List<Event> { event1 };

                eventMock.Setup(em => em.GetEventsByDate(event1.InitialDate)).Returns(todaysMockedEvents);
                sportMock.Setup(sm => sm.GetSportByName(sport.Name, true)).Returns(sport);
                teamMock.Setup(tm => tm.GetTeamByName(team2.Name)).Returns(team2);
                teamMock.Setup(tm => tm.GetTeamByName(team4.Name)).Returns(team4);
                #endregion

                // Instancio UserLogic con el mock como parametro.
                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);

                var newTeamNames = new List<string> { team2.Name, team4.Name };
                eventLogic.AddEvent(sport.Name, newTeamNames, event1.InitialDate);
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

        [TestMethod]
        public void DeleteEventById()
        {
            try
            {
                #region Initialize Mock
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

                Team team1 = new Team { Name = "Nacional", Id = 1 };
                Team team2 = new Team { Name = "Defensor", Id = 2 };
                Team team3 = new Team { Name = "Barcelona", Id = 3 };
                Team team4 = new Team { Name = "Sevilla", Id = 4 };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);

                var ev1Teams = new List<Team> { team1, team3 };
                var ev2Teams = new List<Team> { team2, team1 };
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                event1.Id = 1;

                eventMock.Setup(up => up.GetEventById(1, true)).Returns(event1);
                #endregion

                // Instancio UserLogic con el mock como parametro.
                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                eventLogic.DeleteEventById(1);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
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
                List<Event> foundEvent = eventLogic.GetAllEvents();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryAddEventEventWithDuplicatedTeamNames()
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
                var ev1Teams = new List<Team> { team1, team3 };
                var ev2Teams = new List<Team> { team2, team1 };

                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                Event event2 = new Event(DateTime.Now.AddHours(3), sport, ev2Teams);

                // Events that will be returned for today.
                List<Event> todaysMockedEvents = new List<Event> { event1, event2 };
                eventMock.Setup(em => em.GetEventsByDate(event1.InitialDate)).Returns(todaysMockedEvents);
                sportMock.Setup(sm => sm.GetSportByName(sport.Name, true)).Returns(sport);
                teamMock.Setup(tm => tm.GetTeamByName(team1.Name)).Returns(team1);
                teamMock.Setup(tm => tm.GetTeamByName(team3.Name)).Returns(team2);
                #endregion

                var expectedTeamNames = new List<string> { team1.Name, team1.Name };
                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                eventLogic.AddEvent(sport.Name, expectedTeamNames, event1.InitialDate);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                string expectedEx = string.Format(Constants.EventError.EVENT_TEAM_EXISTS, 0, DateTime.Now.Date);
                Assert.AreEqual(eEx.Message, Constants.SportErrors.REPEATED_TEAMS);
                Assert.AreEqual(eEx.StatusCode, ExceptionStatusCode.InvalidData);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        public void TryToModifyEventWithDuplicateTeamNames()
        {
            try
            {
                #region Initialize Mock
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

                Team team1 = new Team { Name = "Nacional", Id = 1 };
                Team team2 = new Team { Name = "Defensor", Id = 2 };
                Team team3 = new Team { Name = "Barcelona", Id = 3 };
                Team team4 = new Team { Name = "Sevilla", Id = 4 };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);

                var ev1Teams = new List<Team> { team1, team2 };
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                event1.Id = 1;

                // Events that will be returned for today.
                List<Event> todaysMockedEvents = new List<Event> { event1 };

                eventMock.Setup(em => em.GetEventById(It.IsAny<int>(), true)).Returns(event1);
                eventMock.Setup(em => em.GetEventsByDate(It.IsAny<DateTime>())).Returns(todaysMockedEvents);
                #endregion

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                var dummySportNames = new List<string> { team1.Name, team1.Name };

                eventLogic.ModifyEvent(1, dummySportNames, DateTime.Now.AddHours(10));
            }
            catch (EntitiesException eEx)
            {
                Assert.AreEqual(eEx.Message, Constants.SportErrors.REPEATED_TEAMS);
                Assert.AreEqual(eEx.StatusCode, ExceptionStatusCode.InvalidData);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyEventCorrectly()
        {
            try
            {
                #region Initialize Mock
                var eventMock = new Mock<IEventPersistance>();
                var sportMock = new Mock<ISportPersistance>();
                var teamMock = new Mock<ITeamPersistance>();

                Team team1 = new Team { Name = "Nacional", Id = 1 };
                Team team2 = new Team { Name = "Defensor", Id = 2 };
                Team team3 = new Team { Name = "Barcelona", Id = 3 };
                Team team4 = new Team { Name = "Sevilla", Id = 4 };
                List<Team> teams = new List<Team> { team1, team2, team3, team4 };
                Sport sport = new Sport("Football", teams);

                var ev1Teams = new List<Team> { team1, team3 };
                var ev2Teams = new List<Team> { team3, team1 };
                Event event1 = new Event(DateTime.Now, sport, ev1Teams);
                Event event2 = new Event(DateTime.Now.AddHours(3), sport, ev2Teams);
                event1.Id = 1;
                event2.Id = 2;

                // Events that will be returned for today.
                List<Event> todaysMockedEvents = new List<Event> { event1, event2 };

                teamMock.Setup(tm => tm.GetTeamByName("Defensor")).Returns(team2);
                teamMock.Setup(tm => tm.GetTeamByName("Sevilla")).Returns(team4);
                eventMock.Setup(em => em.GetEventById(It.IsAny<int>(), true)).Returns(event1);
                eventMock.Setup(em => em.GetEventsByDate(It.IsAny<DateTime>())).Returns(todaysMockedEvents);
                eventMock.Setup(em => em.ModifyEvent(It.IsAny<Event>())).Verifiable();
                #endregion

                EventLogic eventLogic = new EventLogic(eventMock.Object, sportMock.Object, teamMock.Object);
                string dummySportNameA = team2.Name;
                string dummySportNameB = team4.Name;
                var dummySportsNames = new List<string> { dummySportNameA, dummySportNameB };

                eventLogic.ModifyEvent(1, dummySportsNames, DateTime.Now.AddHours(10));
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

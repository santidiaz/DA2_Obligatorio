using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTests.Utilities;

namespace UnitTests
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void NoParametersEventContructor()
        {
            DateTime expectedDate = DateTime.MinValue;
            var newEvent = new Event();

            Assert.AreEqual(expectedDate.ToString("dd/MM/yyyy HH:mm:ss"), newEvent.InitialDate.ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsTrue(newEvent.Comments.Count.Equals(0));
            Assert.IsTrue(newEvent.EventTeams.Count.Equals(0));
        }

        [TestMethod]
        public void CreateEventWithParameters()
        {
            DateTime expectedDate = DateTime.Now.AddDays(5);
            Team team1 = new Team { Name = "Nacional" };
            Team team2 = new Team { Name = "FC Barcelona" };
            List<Team> expectedTeamList = new List<Team> { team1, team2 };

            Sport expectedSport = new Sport
            {
                Name = "Football",
                Teams = expectedTeamList
            };

            Event newEvent = new Event(expectedDate, expectedSport, expectedTeamList);

            Assert.AreEqual(expectedDate.ToString("dd/MM/yyyy HH:mm:ss"), newEvent.InitialDate.ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsTrue(newEvent.GetLocalTeam().Equals(team1));
            Assert.IsTrue(newEvent.GetAwayTeam().Equals(team2));
            Assert.IsTrue(newEvent.Sport.Equals(expectedSport));
        }

        [TestMethod]
        public void ThrowExceptionOnInvalidDate()
        {
            try
            {
                DateTime badDate = DateTime.Now.AddDays(-2);
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "FC Barcelona" };
                List<Team> teamList = new List<Team> { team1, team2 };
                Sport sport = new Sport
                {
                    Name = "Football",
                    Teams = teamList
                };

                Event newEvent = new Event(badDate, sport, teamList);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.EventError.INVALID_DATE));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyValidTeams()
        {
            DateTime expectedDate = DateTime.Now.AddDays(5);
            Team team1 = new Team { Name = "Nacional" };
            Team team2 = new Team { Name = "FC Barcelona" };
            var teamList = new List<Team> { team1, team2 };
            Team newTema1 = new Team { Name = "Juventus" };
            Team newTema2 = new Team { Name = "PSG" };
            var newTeamList = new List<Team> { newTema1, newTema2 };
            List<Team> expectedTeamList = new List<Team> { team1, team2, newTema1, newTema2 };
            Sport sport = new Sport
            {
                Name = "Football",
                Teams = expectedTeamList
            };

            Event newEvent = new Event(expectedDate, sport, teamList);// Original event
            newEvent.ModifyTeams(newTeamList);// Method to be tested

            Team awayTeam = newEvent.GetAwayTeam();
            Team localTeam = newEvent.GetLocalTeam();
            Assert.IsTrue(localTeam.Equals(newTema1));
            Assert.IsTrue(awayTeam.Equals(newTema2));
        }

        [TestMethod]
        public void InvalidTeamsQuantityOnModify()
        {
            try
            {
                DateTime expectedDate = DateTime.Now.AddDays(5);
                Team team1 = new Team { Name = "Nacional" };
                Team team2 = new Team { Name = "FC Barcelona" };
                Team team3 = new Team { Name = "River" };
                Team team4 = new Team { Name = "Cerro" };
                var defaultEventTeams = new List<Team> { team1, team2 };
                var footballTeams = new List<Team> { team1, team2, team3, team4 };
                Sport sport1 = new Sport
                {
                    Name = "Football",
                    Teams = footballTeams,
                    AllowdMultipleTeamsEvents = false
                };

                var newTeams = new List<Team> { team1, team3, team4 };
                Event newEvent = new Event(expectedDate, sport1, defaultEventTeams);// Original event
                newEvent.ModifyTeams(newTeams);// Method to be tested
            }
            catch (EntitiesException eEx)
            {
                Assert.AreEqual(eEx.Message, Constants.EventError.INVALID_AMOUNT_OF_TEAMS);
                Assert.AreEqual(eEx.StatusCode, ExceptionStatusCode.InvalidData);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void InvalidMultipleEventTeamsQuantityOnModify()
        {
            try
            {
                DateTime expectedDate = DateTime.Now.AddDays(5);
                Team team1 = new Team { Name = "per1" };
                Team team2 = new Team { Name = "per2" };
                Team team3 = new Team { Name = "per3" };
                Team team4 = new Team { Name = "per4" };
                var defaultEventTeams = new List<Team> { team1, team2 };
                var atletismoTeams = new List<Team> { team1, team2, team3, team4 };
                Sport sport1 = new Sport
                {
                    Name = "Atletismo",
                    Teams = atletismoTeams,
                    AllowdMultipleTeamsEvents = true
                };

                var newPlayers = new List<Team> { team2, team4, team1 };
                Event newEvent = new Event(expectedDate, sport1, defaultEventTeams);// Original event
                newEvent.ModifyTeams(newPlayers);
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
        public void InvalidAmountOfTeamsInEvent()
        {
            try
            {
                DateTime expectedDate = DateTime.Now.AddDays(5);
                Team team1 = new Team { Name = "per1" };
                Team team2 = new Team { Name = "per2" };
                Team team3 = new Team { Name = "per3" };
                Team team4 = new Team { Name = "per4" };
                var defaultEventTeams = new List<Team> { team1, team2 };
                var atletismoTeams = new List<Team> { team1, team2, team3, team4 };
                Sport sport1 = new Sport
                {
                    Name = "Atletismo",
                    Teams = atletismoTeams,
                    AllowdMultipleTeamsEvents = true
                };

                Event newEvent = new Event(expectedDate, sport1, defaultEventTeams);
            }
            catch (EntitiesException eEx)
            {
                Assert.AreEqual(eEx.Message, Constants.EventError.INVALID_AMOUNT_OF_TEAMS);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void AddCommentToEvent()
        {
            DateTime expectedDate = DateTime.Now.AddDays(5);
            Team team1 = new Team { Name = "team1" };
            Team team2 = new Team { Name = "team2" };
            Team team3 = new Team { Name = "team3" };
            Team team4 = new Team { Name = "team4" };
            var defaultEventTeams = new List<Team> { team1, team2 };
            var testTeams = new List<Team> { team1, team2, team3, team4 };
            Sport sport1 = new Sport
            {
                Name = "testSport",
                Teams = testTeams,
                AllowdMultipleTeamsEvents = true
            };

            Event newEvent = new Event(expectedDate, sport1, defaultEventTeams);
            newEvent.AddNewComment(
                new Comment
                {
                    CreatorName ="test",
                    Description ="AAA",
                    DatePosted = DateTime.Now,
                    Id = 1
                });

            // Check if test was succesfull.
            var eventComments = newEvent.Comments;
            Assert.IsTrue(eventComments.Exists(c => c.Id.Equals(1)));
        }

        [TestMethod]
        public void GetOrderedCommentsByDateDesc()
        {
            var generatedEvent = Utility.GenerateRandomEvent(true);
            var com1 = new Comment
            {
                CreatorName = "test1",
                Description = "AAA",
                DatePosted = DateTime.Now.AddDays(1),
                Id = 1
            };
            var com2 = new Comment
            {
                CreatorName = "test2",
                Description = "BBB",
                DatePosted = DateTime.Now.AddDays(-3),
                Id = 2
            };
            var com3 = new Comment
            {
                CreatorName = "test3",
                Description = "CCC",
                DatePosted = DateTime.Now.AddDays(5),
                Id = 3
            };

            generatedEvent.Comments.Add(com1);
            generatedEvent.Comments.Add(com2);
            generatedEvent.Comments.Add(com3);

            var orderedComments = generatedEvent.GetOrderedCommentsDesc();
            Assert.AreEqual(orderedComments.First().Id, 3);
        }
    }
}

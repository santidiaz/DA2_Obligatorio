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
        public void CreateEvent()
        {
            DateTime expectedDate = DateTime.Now;
            List<Comment> expectedComments = new List<Comment>();
            Team[] expectedTeams = new Team[2];
                        
            Event newEvent = new Event();

            Assert.AreEqual(expectedDate.ToString("dd/MM/yyyy HH:mm:ss"), newEvent.InitialDate.ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsTrue(Utility.CompareLists(expectedComments, newEvent.GetComments()));
            Assert.IsTrue(newEvent.GetTeams().Length == 2);
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
                TeamsList = expectedTeamList
            };            

            Event newEvent = new Event(expectedDate, expectedSport, team1, team2);

            Assert.AreEqual(expectedDate.ToString("dd/MM/yyyy HH:mm:ss"), newEvent.InitialDate.ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsTrue(newEvent.GetFirstTeam().Equals(team1));
            Assert.IsTrue(newEvent.GetSecondTeam().Equals(team2));
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
                    TeamsList = teamList
                };

                Event newEvent = new Event(badDate, sport, team1, team2);

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
            Team newTema1 = new Team { Name = "Juventus" };
            Team newTema2 = new Team { Name = "PSG" };
            List<Team> expectedTeamList = new List<Team> { team1, team2, newTema1, newTema2 };
            Sport sport = new Sport
            {
                Name = "Football",
                TeamsList = expectedTeamList
            };

            Event newEvent = new Event(expectedDate, sport, team1, team2);// Original event
            bool modificationResult = newEvent.ModifyTeams(newTema1, newTema2);// Method to be tested

            Team[] eventTeams = newEvent.GetTeams();
            Assert.IsTrue(modificationResult);
            Assert.IsTrue(eventTeams.Contains(newTema1));
            Assert.IsTrue(eventTeams.Contains(newTema2));
        }

        [TestMethod]
        public void ModifyInvalidTeamsForSport()
        {
            DateTime expectedDate = DateTime.Now.AddDays(5);
            Team team1 = new Team { Name = "Nacional" };
            Team team2 = new Team { Name = "FC Barcelona" };
            Team newTema1 = new Team { Name = "PSG" };
            Team newTema2 = new Team { Name = "LA Lakers" };
            List<Team> footballTeams = new List<Team> { team1, team2, newTema1 };
            List<Team> basketTeams = new List<Team> { newTema2 };
            Sport sport1 = new Sport
            {
                Name = "Football",
                TeamsList = footballTeams
            };
            Sport sport2 = new Sport
            {
                Name = "Basketball",
                TeamsList = basketTeams
            };

            Event newEvent = new Event(expectedDate, sport1, team1, team2);// Original event
            bool modificationResult = newEvent.ModifyTeams(newTema1, newTema2);// Method to be tested

            Assert.IsFalse(modificationResult);
        }

        [TestMethod]
        public void TryModifyEqualTeams()
        {
            DateTime expectedDate = DateTime.Now.AddDays(5);
            Team team1 = new Team { Name = "Nacional" };
            Team team2 = new Team { Name = "FC Barcelona" };
            Team newTema1 = new Team { Name = "PSG" };
            Team newTema2 = new Team { Name = "LA Lakers" };
            List<Team> footballTeams = new List<Team> { team1, team2, newTema1 };
            List<Team> basketTeams = new List<Team> { newTema2 };
            Sport sport1 = new Sport
            {
                Name = "Football",
                TeamsList = footballTeams
            };
            Sport sport2 = new Sport
            {
                Name = "Basketball",
                TeamsList = basketTeams
            };

            Event newEvent = new Event(expectedDate, sport1, team1, team2);// Original event
            bool modificationResult = newEvent.ModifyTeams(newTema1, newTema2);// Method to be tested

            Assert.IsFalse(modificationResult);
        }

        [TestMethod]
        public void AddNewCommentToEvent()
        {
            Event newEvent = Utility.GenerateRandomEvent();
            Comment newComment = new Comment
            {
                CreatorName = "santidiaz",
                Description = "aaaaaaaa"
            };

            newEvent.AddComment(newComment);
            List<Comment> eventComments = newEvent.GetComments();

            Assert.IsTrue(eventComments.Contains(newComment));
        }
        #region Private Helpers
        #endregion
    }
}

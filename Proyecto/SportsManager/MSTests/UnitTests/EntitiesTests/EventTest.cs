using BusinessEntities;
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
            Team[] eventTeams = newEvent.GetTeams();

            Assert.AreEqual(expectedDate.ToString("dd/MM/yyyy HH:mm:ss"), newEvent.InitialDate.ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.IsTrue(eventTeams.Contains(team1));
            Assert.IsTrue(eventTeams.Contains(team2));
            Assert.IsTrue(newEvent.Sport.Equals(expectedSport));
        }

        #region Private Helpers
        #endregion
    }
}

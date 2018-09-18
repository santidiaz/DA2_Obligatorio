using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class EventTest 
    {
        [TestMethod]
        public void CreateEvent()
        {
            var expectedDate = DateTime.Now;
            var expectedSport = new Sport();
            var exceptedTeam1 = new Team();
            var exceptedTeam2 = new Team();
            var exceptedComments = "comments";

            Event _event = new Event();
            _event.Date = expectedDate;
            _event.Sport = expectedSport;
            _event.Team1 = exceptedTeam1;
            _event.Team2 = exceptedTeam2;
            _event.Comments = exceptedComments;

            Assert.AreEqual(_event.Date, expectedDate);
            Assert.AreEqual(_event.Sport, expectedSport);
            Assert.AreEqual(_event.Team1, exceptedTeam1);
            Assert.AreEqual(_event.Team2, exceptedTeam2);
            Assert.AreEqual(_event.Comments, exceptedComments);
        }
    }
}

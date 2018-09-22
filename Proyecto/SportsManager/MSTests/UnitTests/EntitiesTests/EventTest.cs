using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}

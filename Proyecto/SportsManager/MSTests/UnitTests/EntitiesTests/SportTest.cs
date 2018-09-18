using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class SportTest
    {
        [TestMethod]
        public void CreateSport()
        {
            var expectedName = "Futbol";
            var expectedTeamsList = new List<Team>() { new Team() { Name = "a", Photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } };

            Sport sport = new Sport();
            sport.Name = expectedName;
            sport.TeamsList = expectedTeamsList;

            Assert.AreEqual(sport.Name, expectedName);
            Assert.AreEqual(sport.TeamsList, expectedTeamsList);
        }
    }
}

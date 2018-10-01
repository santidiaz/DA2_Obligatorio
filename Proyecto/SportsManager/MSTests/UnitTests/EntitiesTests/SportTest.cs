using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Utilities;

namespace UnitTests
{
    [TestClass]
    public class SportTest
    {
        [TestMethod]
        public void CreateSport()
        {
            var expectedName = Constants.Sport.NAME_SPORT_FUTBOL;
            var expectedTeamsList = new List<Team>() { new Team() { Name = "Racing", Photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } };

            Sport sport = new Sport();
            sport.Name = expectedName;
            sport.Teams = expectedTeamsList;

            Assert.AreEqual(sport.Name, expectedName);
            Assert.AreEqual(sport.Teams, expectedTeamsList);
        }

        [TestMethod]
        public void ThrowExceptionOnCreateSportNameRequired()
        {
            try
            {
                var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

                Sport sport = new Sport();
                sport.Name = string.Empty;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.NAME_REQUIRED));
            }
        }

        [TestMethod]
        public void ThrowExceptionOnCreateInvalidTeamListRequired()
        {
            try
            {
                Sport sport = new Sport();
                sport.Name = Utilities.Utility.GetRandomSportName();
                sport.TeamsList = null;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.SportErrors.TEAMLIST_REQUIRED));
            }
        }

        [TestMethod]
        public void CreateTeamWithParameters()
        {
            var expectedName = Constants.Sport.NAME_SPORT_FUTBOL;
            var expectedTeamList = new List<Team>() { new Team() { Name = "Racing" } };

            Sport sport = new Sport(expectedName, expectedTeamList);

            Assert.AreEqual(expectedName, sport.Name);
            Assert.AreEqual(expectedTeamList, sport.TeamsList);
        }

        [TestMethod]
        public void SportInstancesAreEqual()
        {
            string name = Utility.GetRandomSportName();
            var teamList = new List<Team>() { new Team() { Name = "Racing" } };
            Sport firstSport = new Sport(name, teamList);
            Sport secondSport = new Sport(name, teamList);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SportsInstancesAreNotEqual()
        {
            string name1 = Constants.Sport.NAME_SPORT_FUTBOL;
            var teamList1 = new List<Team>() { new Team() { Name = "Racing" } };

            string name2 = Constants.Sport.NAME_SPORT_TENIS;
            var teamList2 = new List<Team>() { new Team() { Name = "River" } };

            Sport firstSport = new Sport(name1, teamList1);
            Sport secondSport = new Sport(name2, teamList2);

            Assert.IsFalse(firstSport.Equals(secondSport));
        }
    }
}

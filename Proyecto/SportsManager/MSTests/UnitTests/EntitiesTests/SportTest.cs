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
            var expectedName = Constants.Sport.NAME_SPORT;
            var expectedTeamsList = new List<Team>() { new Team() { Name = "Racing", Photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } };

            Sport sport = new Sport();
            sport.Name = expectedName;
            sport.TeamsList = expectedTeamsList;

            Assert.AreEqual(sport.Name, expectedName);
            Assert.AreEqual(sport.TeamsList, expectedTeamsList);
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

        //[TestMethod]
        //public void CreateTeamPhotoInvalid()
        //{
        //    var expectedName = Constants.Team.NAME_TEST;
        //    var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        //    Team team = new Team();
        //    team.Name = expectedName;
        //    team.Photo = expectedPhoto;

        //    Assert.AreEqual(team.Name, expectedName);
        //    Assert.AreEqual(team.Photo, expectedPhoto);
        //}
        
        //[TestMethod]
        //public void CreateTeamWithParameters()
        //{
        //    var expectedName = Constants.Team.NAME_TEST;
        //    var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        //    Team team = new Team(expectedName, expectedPhoto);

        //    Assert.AreEqual(expectedName, team.Name);
        //    Assert.AreEqual(expectedPhoto, team.Photo);
        //}

        //[TestMethod]
        //public void TeamsInstancesAreEqual()
        //{
        //    string name = Utility.GetRandomName();
        //    byte[] photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
        //    Team firstTeam = new Team(name, photo);
        //    Team secondTeam = new Team(name, photo);

        //    Assert.IsTrue(firstTeam.Equals(secondTeam));
        //}

        //[TestMethod]
        //public void TeamsInstancesAreNotEqual()
        //{
        //    string name1 = Constants.Team.NAME_TEST;
        //    byte[] photo1 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        //    string name2 = Constants.Team.NAME_TEST_PENIAROL;
        //    byte[] photo2 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        //    Team firstTeam = new Team(name1, photo1);
        //    Team secondTeam = new Team(name2, photo2);

        //    Assert.IsFalse(firstTeam.Equals(secondTeam));
        //}
    }
}

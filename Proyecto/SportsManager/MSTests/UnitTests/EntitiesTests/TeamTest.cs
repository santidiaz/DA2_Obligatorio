using System;
using System.Collections.Generic;
using System.Text;
using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Utilities;
using System.Linq;

namespace UnitTests.EntitiesTests
{
    [TestClass]
    public class TeamTest
    {
        [TestInitialize]
        public void TestInitialization()
        {
            //SystemDummyData.GetInstance.Reset();
        }

        [TestMethod]
        public void ThrowExceptionOnCreateTeamNameRequired()
        {
            try
            {
                var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

                Team team = new Team();
                team.Name = string.Empty;
                team.Photo = expectedPhoto;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.NAME_REQUIRED));
            }
        }

        [TestMethod]
        public void ThrowExceptionOnCreateInvalidPhotoRequired()
        {
            try
            {
                Team team = new Team();
                team.Name = Utilities.Utility.GetRandomTeamName();
                team.Photo = null;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.PHOTO_INVALID));
            }
        }

        [TestMethod]
        public void CreateTeamPhotoInvalid()
        {
            var expectedName = Constants.Team.NAME_TEST;
            var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Team team = new Team();
            team.Name = expectedName;
            team.Photo = expectedPhoto;

            Assert.AreEqual(team.Name, expectedName);
            Assert.AreEqual(team.Photo, expectedPhoto);
        }

        [TestMethod]
        public void CreateTeam()
        {
            var expectedName = Constants.Team.NAME_TEST;
            var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Team team = new Team();
            team.Name = expectedName;
            team.Photo = expectedPhoto;

            Assert.AreEqual(team.Name, expectedName);
            Assert.AreEqual(team.Photo, expectedPhoto);
        }

        [TestMethod]
        public void CreateTeamWithParameters()
        {
            var expectedName = Constants.Team.NAME_TEST;
            var expectedPhoto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Team team = new Team(expectedName, expectedPhoto);

            Assert.AreEqual(expectedName, team.Name);
            Assert.AreEqual(expectedPhoto, team.Photo);
        }

        [TestMethod]
        public void TeamsInstancesAreEqual()
        {
            string name = Utility.GetRandomName();
            byte[] photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            Team firstTeam = new Team(name, photo);
            Team secondTeam = new Team(name, photo);

            Assert.IsTrue(firstTeam.Equals(secondTeam));
        }

        [TestMethod]
        public void TeamsInstancesAreNotEqual()
        {
            string name1 = Constants.Team.NAME_TEST;
            byte[] photo1 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            string name2 = Constants.Team.NAME_TEST_PENIAROL;
            byte[] photo2 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Team firstTeam = new Team(name1, photo1);
            Team secondTeam = new Team(name2, photo2);

            Assert.IsFalse(firstTeam.Equals(secondTeam));
        }

    }
}
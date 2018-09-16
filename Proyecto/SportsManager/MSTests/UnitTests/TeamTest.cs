using System;
using System.Collections.Generic;
using System.Text;
using BusinessContracts;
using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderManager;
using UnitTests.Utilities;

namespace UnitTests
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void CreateTeam()
        {
            var expectedName = "Nacional";
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
            var expectedName = "Nacional";
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
            string name1 = "Nacional";
            byte[] photo1 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            string name2 = "Peñarol";
            byte[] photo2 = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Team firstTeam = new Team(name1, photo1);
            Team secondTeam = new Team(name2, photo2);

            Assert.IsFalse(firstTeam.Equals(secondTeam));
        }

        [TestMethod]
        public void AddTeamToSystem()
        {
            ITeamLogic TeamOperations = Provider.GetInstance.GetTeamOperations();

            Team newTeam = new Team("Nacional", new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });

            TeamOperations.AddTeam(newTeam);

            Assert.IsNotNull(this.FindTeamOnSystem(newTeam.Name));
        }
        
        private object FindTeamOnSystem(string name)
        {
            ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();
            List<Team> activities = teamOperations.GetTeams();
            return activities.Find(x => x.Name == name);
        }

    }
}

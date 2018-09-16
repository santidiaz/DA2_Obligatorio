using System;
using System.Collections.Generic;
using System.Text;
using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderManager;
using UnitTests.Utilities;
using System.Linq;

namespace UnitTests
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

        [TestMethod]
        public void AddTeamToSystem()
        {
            ITeamLogic TeamOperations = Provider.GetInstance.GetTeamOperations();

            Team newTeam = new Team(Constants.Team.NAME_TEST, new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });

            TeamOperations.AddTeam(newTeam);

            Assert.IsNotNull(this.FindTeamOnSystem(newTeam.Name));
        }
        
        private object FindTeamOnSystem(string name)
        {
            ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();
            List<Team> activities = teamOperations.GetTeams();
            return activities.Find(x => x.Name == name);
        }
        
        [TestMethod]
        public void TryToAddTeamThatAlreadyExistsToSystem()
        {
            ITeamLogic TeamOperations = Provider.GetInstance.GetTeamOperations();

            Team newTeam = new Team("Nacional", new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });

            TeamOperations.AddTeam(newTeam);

            Assert.IsNotNull(this.FindTeamOnSystem(newTeam.Name));
        }

        [TestMethod]
        public void ModifyTeam()
        {
            ITeamLogic TeamOperations = Provider.GetInstance.GetTeamOperations();

            var team = new Team(Constants.Team.NAME_TEST, new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });
            TeamOperations.AddTeam(team);

            string teamName = team.Name;

            team.Name = Constants.Team.NAME_TEST;
            TeamOperations.ModifyTeamByName(teamName, team);

            var modifiedTeam = TeamOperations.GetTeamByName(team.Name);
            Assert.AreEqual(modifiedTeam.Name, Constants.Team.NAME_TEST_MODIFY);
        }

        [TestMethod]
        public void DeleteTeamByName()
        {
            ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();

            var team = new Team(Constants.Team.NAME_TEST, new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });
            teamOperations.AddTeam(team);

            teamOperations.DeleteTeamByName(team.Name);

            var quantityOfATeams = teamOperations.GetTeams().Count();

            Assert.IsTrue(quantityOfATeams == 0);
        }
    }
}

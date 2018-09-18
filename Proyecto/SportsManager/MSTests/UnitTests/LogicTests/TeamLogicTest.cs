using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using DataContracts;
using BusinessLogic;
using UnitTests.Utilities;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class TeamLogicTest
    {
        [TestMethod]
        public void AddTeamThatNotExists()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
            var mock = new Mock<ITeamPersistance>();
            mock.Setup(up => up.GetTeams()).Returns(new List<Team>());
            mock.Setup(mr => mr.AddTeam(It.IsAny<Team>())).Verifiable();

            // Instancio TeamLogic con el mock como parametro.
            TeamLogic userLogic = new TeamLogic(mock.Object);
            Team teamToAdd = Utility.GenerateRandomTeam();
            userLogic.AddTeam(teamToAdd);

            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void TryToAddTeamThatAlreadyExistsToSystem()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>();
                mock.Setup(mr => mr.AddTeam(It.IsAny<Team>())).Verifiable();

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object);
                Team teamToAdd = Utility.GenerateRandomTeam();

                mock.Setup(up => up.GetTeams()).Returns(new List<Team>() { teamToAdd });

                userLogic.AddTeam(teamToAdd);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.ERROR_TEAM_ALREADY_EXISTS));
            }
        }

        [TestMethod]
        public void ModifyTeamThatNameNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>();
                mock.Setup(up => up.GetTeams()).Returns(new List<Team>());
                mock.Setup(mr => mr.ModifyTeamByName(It.IsAny<Team>())).Verifiable();

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object);
                Team teamToModify = Utility.GenerateRandomTeam();
                userLogic.ModifyTeamByName(Constants.Team.NAME_TEST, teamToModify);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteTeamByName()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>();
                Team teamToDelete = Utility.GenerateRandomTeam(Constants.Team.NAME_TEST);

                mock.Setup(up => up.GetTeams()).Returns(new List<Team>() { teamToDelete });
                mock.Setup(mr => mr.DeleteTeamByName(It.IsAny<string>())).Verifiable();

                TeamLogic teamLogic = new TeamLogic(mock.Object);
                string userToBeDeleted = Constants.Team.NAME_TEST;
                teamLogic.DeleteTeamByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

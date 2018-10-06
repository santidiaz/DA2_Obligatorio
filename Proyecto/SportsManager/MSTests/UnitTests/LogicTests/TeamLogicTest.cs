using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
            mock.Setup(up => up.GetTeams()).Returns(new List<Team>());
            mock.Setup(mr => mr.AddTeam(It.IsAny<Team>(), 1)).Verifiable();
            mockSport.Setup(mks => mks.GetSports()).Returns(new List<Sport>() { new Sport { SportOID = 1 } });

            // Instancio TeamLogic con el mock como parametro.
            TeamLogic userLogic = new TeamLogic(mock.Object , mockSport.Object);
            Team teamToAdd = Utility.GenerateRandomTeam();
            userLogic.AddTeam(teamToAdd, 1);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TryToAddTeamThatAlreadyExistsToSystem()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                mock.Setup(mr => mr.AddTeam(It.IsAny<Team>(), 1)).Verifiable();
                mockSport.Setup(mks => mks.GetSports()).Returns(new List<Sport>() { new Sport { SportOID = 1 } });

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object , mockSport.Object);
                Team teamToAdd = Utility.GenerateRandomTeam();

                mock.Setup(up => up.GetTeams()).Returns(new List<Team>() { teamToAdd });

                userLogic.AddTeam(teamToAdd, 1);

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
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                mock.Setup(up => up.GetTeamByName(It.IsAny<string>())).Returns(new Team());
                mock.Setup(mr => mr.ModifyTeamByName(It.IsAny<string>(), It.IsAny<Team>())).Verifiable();

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object , mockSport.Object);
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
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                Team teamToDelete = Utility.GenerateRandomTeam(Constants.Team.NAME_TEST);

                mock.Setup(up => up.GetTeams()).Returns(new List<Team>() { teamToDelete });
                mock.Setup(mr => mr.DeleteTeamByName(It.IsAny<Team>())).Verifiable();

                TeamLogic teamLogic = new TeamLogic(mock.Object, mockSport.Object);
                string userToBeDeleted = Constants.Team.NAME_TEST;
                teamLogic.DeleteTeamByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetEventsByTeam()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                Team team = Utility.GenerateRandomTeam(Constants.Team.NAME_TEST);

                mock.Setup(up => up.GetTeamByName(It.IsAny<string>())).Returns(new Team());
                mock.Setup(up => up.GetEventsByTeam(It.IsAny<Team>())).Returns(new List<Event>());

                TeamLogic sportLogic = new TeamLogic(mock.Object, mockSport.Object);
                sportLogic.GetEventsByTeam(team.Name);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void GetEventsByTeamThatNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                Team team = Utility.GenerateRandomTeam(Constants.Team.NAME_TEST);

                //TODO : Como mockeo un retorno NULL ?
                mock.Setup(up => up.GetTeamByName(It.IsAny<string>())).Returns(new Team());
                mock.Setup(up => up.GetEventsByTeam(It.IsAny<Team>())).Returns(new List<Event>());

                TeamLogic sportLogic = new TeamLogic(mock.Object, mockSport.Object);
                sportLogic.GetEventsByTeam(team.Name);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void GetTeamByOID()
        {
            try
            {
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                
                mock.Setup(up => up.GetTeamByOID(It.IsAny<int>())).Returns(new Team());

                TeamLogic userLogic = new TeamLogic(mock.Object , mockSport.Object);
                userLogic.GetTeamByOID(1);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void GetTeamByOIDAndNotExists()
        {
            try
            {
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();

                mock.Setup(up => up.GetTeamByOID(It.IsAny<int>())).Verifiable();

                TeamLogic userLogic = new TeamLogic(mock.Object , mockSport.Object);
                userLogic.GetTeamByOID(1);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.ERROR_TEAM_NOT_EXISTS));
            }
        }

        [TestMethod]
        public void AddTeamThatNotSportOIDInvalid()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                mock.Setup(up => up.GetTeams()).Returns(new List<Team>());
                mock.Setup(mr => mr.AddTeam(It.IsAny<Team>(), 1)).Verifiable();

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object, mockSport.Object);
                Team teamToAdd = Utility.GenerateRandomTeam();
                userLogic.AddTeam(teamToAdd, -1);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.TeamErrors.TEAM_SPORTOID_FAIL));
            }
        }

        [TestMethod]
        public void AddTeamThatNotSportOIDNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                mock.Setup(up => up.GetTeams()).Returns(new List<Team>());
                mock.Setup(mr => mr.AddTeam(It.IsAny<Team>(), 1)).Verifiable();
                mockSport.Setup(mks => mks.GetSports()).Returns(new List<Sport>() { new Sport { SportOID = 0 } });

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object, mockSport.Object);
                Team teamToAdd = Utility.GenerateRandomTeam();
                userLogic.AddTeam(teamToAdd, 1);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ValidateTeamOnEvents()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ITeamPersistance>(); var mockSport = new Mock<ISportPersistance>();
                mock.Setup(up => up.ValidateTeamOnEvents(It.IsAny<Team>())).Returns(true);

                // Instancio TeamLogic con el mock como parametro.
                TeamLogic userLogic = new TeamLogic(mock.Object, mockSport.Object);
                Team teamToAdd = Utility.GenerateRandomTeam();
                bool result = userLogic.ValidateTeamOnEvents(teamToAdd);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS));
            }
        }
    }
}
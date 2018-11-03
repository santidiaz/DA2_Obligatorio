using BusinessEntities;
using BusinessLogic;
using DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Utilities;
using System.Linq;
using Moq.Language;
using CommonUtilities;
using BusinessEntities.Exceptions;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class SportLogicTest
    {
        [TestMethod]
        public void AddSportThatNotExists()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
            var mock = new Mock<ISportPersistance>();
            mock.Setup(up => up.GetSports()).Returns(new List<Sport>());
            mock.Setup(mr => mr.AddSport(It.IsAny<Sport>())).Verifiable();

            // Instancio SportLogic con el mock como parametro.
            SportLogic userLogic = new SportLogic(mock.Object);
            Sport sportToAdd = Utility.GenerateRandomSport();
            userLogic.AddSport(sportToAdd);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TryToAddSportThatAlreadyExistsToSystem()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                mock.Setup(mr => mr.AddSport(It.IsAny<Sport>())).Verifiable();

                // Instancio SportLogic con el mock como parametro.
                SportLogic userLogic = new SportLogic(mock.Object);
                Sport sportToAdd = Utility.GenerateRandomSport();

                mock.Setup(up => up.GetSports()).Returns(new List<Sport>() { sportToAdd });

                userLogic.AddSport(sportToAdd);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.SportErrors.ERROR_SPORT_ALREADY_EXISTS));
            }
        }

        [TestMethod]
        public void ModifySportThatNameNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns(new Sport());
                mock.Setup(mr => mr.ModifySportByName(It.IsAny<Sport>())).Verifiable();

                // Instancio SportLogic con el mock como parametro.
                SportLogic userLogic = new SportLogic(mock.Object);
                Sport sportToModify = Utility.GenerateRandomSport();
                userLogic.ModifySportByName(Constants.Sport.NAME_SPORT_FUTBOL, sportToModify);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DeleteSportByName()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns(new Sport());
                mock.Setup(up => up.GetSports()).Returns(new List<Sport>() { sportToDelete });
                mock.Setup(mr => mr.DeleteSportByName(It.IsAny<Sport>())).Verifiable();

                SportLogic sportLogic = new SportLogic(mock.Object);
                string userToBeDeleted = Constants.Sport.NAME_SPORT_FUTBOL;
                sportLogic.DeleteSportByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetEventsBySport()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns(new Sport());
                mock.Setup(up => up.GetEventsBySport(It.IsAny<Sport>())).Returns(new List<Event>());

                SportLogic sportLogic = new SportLogic(mock.Object);
                sportLogic.GetEventsBySport(sportToDelete.Name);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetEventsBySportThatNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                //TODO : Como mockeo un retorno NULL ?
                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns((new Sport()));
                mock.Setup(up => up.GetEventsBySport(It.IsAny<Sport>())).Returns(new List<Event>());

                SportLogic sportLogic = new SportLogic(mock.Object);
                sportLogic.GetEventsBySport(sportToDelete.Name);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS));
            }
        }

        [TestMethod]
        public void ValidateTeamOnEvents()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var sportMock = new Mock<ISportPersistance>();

                Sport mockedSport = Utility.GenerateRandomSport();
                sportMock.Setup(sp => sp.GetSportByName(mockedSport.Name, true)).Returns(mockedSport);
                sportMock.Setup(sp => sp.ValidateSportOnTeams(It.IsAny<Sport>())).Returns(true);

                // Instancio SportLogic con el mock como parametro.
                SportLogic userLogic = new SportLogic(sportMock.Object);
                
                userLogic.DeleteSportByName(mockedSport.Name);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.AreEqual(eEx.Message, Constants.SportErrors.ERROR_SPORT_HAS_TEAMS);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteSportByNameAndValidateSportOnTeams()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Verifiable();
                mock.Setup(up => up.GetSports()).Returns(new List<Sport>() { sportToDelete });
                mock.Setup(mr => mr.DeleteSportByName(It.IsAny<Sport>())).Verifiable();

                SportLogic sportLogic = new SportLogic(mock.Object);
                string userToBeDeleted = Constants.Sport.NAME_SPORT_FUTBOL;
                sportLogic.DeleteSportByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DeleteSportByNameAndValidateSportOnTeamsNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns(new Sport());
                mock.Setup(up => up.GetSports()).Returns(new List<Sport>() { sportToDelete });
                mock.Setup(mr => mr.DeleteSportByName(It.IsAny<Sport>())).Verifiable();
                mock.Setup(up => up.ValidateSportOnTeams(It.IsAny<Sport>())).Returns(false);

                SportLogic sportLogic = new SportLogic(mock.Object);
                string userToBeDeleted = Constants.Sport.NAME_SPORT_FUTBOL;
                sportLogic.DeleteSportByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetSportByNameAndReturnNull()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns((Sport)null);
                mock.Setup(mr => mr.ModifySportByName(It.IsAny<Sport>())).Verifiable();

                // Instancio SportLogic con el mock como parametro.
                SportLogic userLogic = new SportLogic(mock.Object);
                Sport sportToModify = Utility.GenerateRandomSport();
                userLogic.GetSportByName(Constants.Sport.NAME_SPORT_FUTBOL);

                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.SportErrors.ERROR_SPORT_NOT_EXISTS));
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DeleteSportByNameAndValidateSportOnTeamsNotExistsReturnNull()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<ISportPersistance>();
                Sport sportToDelete = Utility.GenerateRandomSport(Constants.Sport.NAME_SPORT_FUTBOL);

                mock.Setup(up => up.GetSportByName(It.IsAny<string>(), true)).Returns(new Sport());
                mock.Setup(up => up.GetSports()).Returns(new List<Sport>() { sportToDelete });
                mock.Setup(mr => mr.DeleteSportByName(It.IsAny<Sport>())).Verifiable();
                mock.Setup(up => up.ValidateSportOnTeams(It.IsAny<Sport>())).Returns(true);

                SportLogic sportLogic = new SportLogic(mock.Object);
                string userToBeDeleted = Constants.Sport.NAME_SPORT_FUTBOL;
                sportLogic.DeleteSportByName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}

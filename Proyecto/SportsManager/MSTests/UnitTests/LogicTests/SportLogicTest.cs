﻿using BusinessEntities;
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

namespace UnitTests.LogicTests
{
    [TestClass]
    public class SportLogicTest
    {
        [TestMethod]
        public void AddSportThatNotExists()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
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
                mock.Setup(up => up.GetSportByName(It.IsAny<string>())).Returns(new Sport());
                mock.Setup(mr => mr.ModifySportByName(It.IsAny<string>(), It.IsAny<Sport>())).Verifiable();

                // Instancio SportLogic con el mock como parametro.
                SportLogic userLogic = new SportLogic(mock.Object);
                Sport sportToModify = Utility.GenerateRandomSport();
                userLogic.ModifySportByName(Constants.Sport.NAME_SPORT_FUTBOL, sportToModify);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

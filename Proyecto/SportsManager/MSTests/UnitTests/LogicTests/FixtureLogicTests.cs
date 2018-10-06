using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class FixtureLogicTests
    {
        [TestMethod]
        public void RoundRobinTest()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
            //var mockUserPersitance = new Mock<IUserPersistance>();
            //var mockTeamPersitance = new Mock<ITeamPersistance>();
            //var mockUserName = "santidiaz";
            //mockUserPersitance.Setup(up => up.DoesUserExists(mockUserName)).Returns(true);

            //// Instancio UserLogic con el mock como parametro.
            //UserLogic userLogic = new FixtureLogi(mockUserPersitance.Object, mockTeamPersitance.Object);

            //string userNameThatExists = "santidiaz";
            //string userNameThatDoNotExists = "abcdef";
            //bool resultExists = userLogic.DoesUserExists(userNameThatExists);
            //bool resultNotExists = userLogic.DoesUserExists(userNameThatDoNotExists);

            //Assert.AreEqual(true, resultExists);
            //Assert.AreEqual(false, resultNotExists);
        }
    }
}
using BusinessLogic;
using DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void DoesUserExists()
        {            
            // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
            var mock = new Mock<IUserPersistance>();
            var mockUserName = "santidiaz";
            mock.Setup(p => p.DoesUserExists(mockUserName)).Returns(false);

            // Instancio UserLogic con el mock como parametro.
            UserLogic userLogic = new UserLogic(mock.Object);
            string userNameToValidate = "santidiaz";
            bool expectedResult = false;
            bool result = userLogic.DoesUserExists(userNameToValidate);

            Assert.AreEqual(expectedResult, result);
        }
    }
}

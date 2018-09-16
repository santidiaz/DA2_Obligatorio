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
            mock.Setup(p => p.DoesUserExists(mockUserName)).Returns(true);

            // Instancio UserLogic con el mock como parametro.
            UserLogic userLogic = new UserLogic(mock.Object);

            string userNameThatExists = "santidiaz";
            string userNameThatDoNotExists = "abcdef";
            bool resultExists = userLogic.DoesUserExists(userNameThatExists);
            bool resultNotExists = userLogic.DoesUserExists(userNameThatDoNotExists);

            Assert.AreEqual(true, resultExists);
            Assert.AreEqual(false, resultNotExists);
        }

        //[TestMethod]
        //public void UserAlreadyExists()
        //{
        //    // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
        //    var mock = new Mock<IUserPersistance>();
        //    var mockUserName = "santidiaz";
        //    mock.Setup(p => p.DoesUserExists(mockUserName)).Returns(true);

        //    // Instancio UserLogic con el mock como parametro.
        //    UserLogic userLogic = new UserLogic(mock.Object);
        //    string userNameToValidate = "santidiaz";
        //    bool expectedResult = true;
        //    bool result = userLogic.DoesUserExists(userNameToValidate);

        //    Assert.AreEqual(expectedResult, result);
        //}
    }
}

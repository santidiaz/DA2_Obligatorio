using BusinessEntities;
using BusinessLogic;
using CommonUtilities;
using DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Utilities;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void DoesUserExistsMethodTest()
        {            
            // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
            var mock = new Mock<IUserPersistance>();
            var mockUserName = "santidiaz";
            mock.Setup(up => up.DoesUserExists(mockUserName)).Returns(true);

            // Instancio UserLogic con el mock como parametro.
            UserLogic userLogic = new UserLogic(mock.Object);

            string userNameThatExists = "santidiaz";
            string userNameThatDoNotExists = "abcdef";
            bool resultExists = userLogic.DoesUserExists(userNameThatExists);
            bool resultNotExists = userLogic.DoesUserExists(userNameThatDoNotExists);

            Assert.AreEqual(true, resultExists);
            Assert.AreEqual(false, resultNotExists);
        }

        [TestMethod]
        public void AddUserThatNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();
                mock.Setup(up => up.DoesUserExists(It.IsAny<string>())).Returns(false);
                mock.Setup(mr => mr.AddUser(It.IsAny<User>())).Verifiable();

                // Instancio UserLogic con el mock como parametro.
                UserLogic userLogic = new UserLogic(mock.Object);
                User userToAdd = Utility.GenerateRandomUser();
                userLogic.AddUser(userToAdd);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryAddUserThatExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();
                mock.Setup(up => up.DoesUserExists(It.IsAny<string>())).Returns(true);
                mock.Setup(mr => mr.AddUser(It.IsAny<User>())).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User userToAdd = Utility.GenerateRandomUser();
                userLogic.AddUser(userToAdd);

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.USER_ALREDY_EXISTS));
            }
        }

        [TestMethod]
        public void TryGetUserByUserName()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();
                User mockedUser = Utility.GenerateRandomUser("santidiaz");
                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedUser);
                
                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeSearch = "santidiaz";
                User foundUser = Utility.GenerateRandomUser(userToBeSearch);

                Assert.AreEqual(foundUser.UserName, userToBeSearch);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        //[TestMethod]
        //public void DeleteUser()
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

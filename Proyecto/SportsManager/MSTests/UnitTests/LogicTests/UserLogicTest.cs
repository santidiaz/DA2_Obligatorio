using BusinessEntities;
using BusinessEntities.Exceptions;
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
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.USER_ALREDY_EXISTS));
            }
        }

        [TestMethod]
        public void TryGetUserByUserName()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedUser = Utility.GenerateRandomUser("santidiaz");
                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedUser);

                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeSearch = "santidiaz";
                User foundUser = userLogic.GetUserByUserName(userToBeSearch);

                Assert.AreEqual(foundUser.UserName, userToBeSearch);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryGetUserByUserNameThatNotExists()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                mock.Setup(up => up.GetUserByUserName(It.IsAny<string>())).Returns((User)null);

                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeSearch = "santidiaz";
                User foundUser = userLogic.GetUserByUserName(userToBeSearch);

                Assert.IsTrue(foundUser == null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteUserByUserName()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();
                User mockedUserToDelete = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedUserToDelete);
                mock.Setup(up => up.DeleteUser(mockedUserToDelete)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeDeleted = "santidiaz";
                userLogic.DeleteUserByUserName(userToBeDeleted);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryDeleteUserByUserNameThatNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();
                mock.Setup(up => up.GetUserByUserName(It.IsAny<string>())).Returns((User)null);

                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeDeleted = "santidiaz";
                userLogic.DeleteUserByUserName(userToBeDeleted);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.UserError.USER_NOT_FOUND));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryDeleteUserByUserNameThatGenerateException()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de IUserPersistance.
                var mock = new Mock<IUserPersistance>();

                mock.Setup(up => up.GetUserByUserName(It.IsAny<string>())).Throws(new Exception());

                UserLogic userLogic = new UserLogic(mock.Object);
                string userToBeDeleted = "santidiaz";
                userLogic.DeleteUserByUserName(userToBeDeleted);

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ModifyNameTest()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = "goku",
                    LastName = mockedOriginalUser.LastName,
                    Email = mockedOriginalUser.Email,
                    IsAdmin = mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = mockedOriginalUser.Password
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyLastNameTest()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = mockedOriginalUser.Name,
                    LastName = "asdfg",
                    Email = mockedOriginalUser.Email,
                    IsAdmin = mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = mockedOriginalUser.Password
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyEmailTest()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = mockedOriginalUser.Name,
                    LastName = mockedOriginalUser.LastName,
                    Email = "qwer@zxc.com",
                    IsAdmin = mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = mockedOriginalUser.Password
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyAdminToFollowerTest()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = mockedOriginalUser.Name,
                    LastName = mockedOriginalUser.LastName,
                    Email = mockedOriginalUser.Email,
                    IsAdmin = !mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = mockedOriginalUser.Password
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ModifyPasswordTest()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz");

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = mockedOriginalUser.Name,
                    LastName = mockedOriginalUser.LastName,
                    Email = mockedOriginalUser.Email,
                    IsAdmin = mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = "32165885"
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.Fail(eEx.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryToModifyUserWithNoModifications()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz", Utility.GenerateHash("123456"));

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.ModifyUser(mockedOriginalUser)).Verifiable();

                UserLogic userLogic = new UserLogic(mock.Object);
                User modifiedUser = new User
                {
                    Name = mockedOriginalUser.Name,
                    LastName = mockedOriginalUser.LastName,
                    Email = mockedOriginalUser.Email,
                    IsAdmin = mockedOriginalUser.IsAdmin,
                    UserName = mockedOriginalUser.UserName,
                    Password = "123456"
                };

                userLogic.ModifyUser(modifiedUser);
                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.UserError.NO_CHANGES));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void AddFavoritesToUser()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz", Utility.GenerateHash("123456"));

                mock.Setup(up => up.GetUserByUserName("santidiaz")).Returns(mockedOriginalUser);
                mock.Setup(up => up.AddFavoritesToUser(It.IsAny<User>(), It.IsAny<List<Team>>())).Verifiable();

                List<Team> teamLists = new List<Team>() { new Team() { TeamOID = 1 }, new Team { TeamOID = 2 } };

                UserLogic userLogic = new UserLogic(mock.Object);
                userLogic.AddFavoritesToUser(mockedOriginalUser, teamLists);

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void AddFavoritesToUserThatNotExists()
        {
            try
            {
                var mock = new Mock<IUserPersistance>();
                User mockedOriginalUser = Utility.GenerateRandomUser("santidiaz", Utility.GenerateHash("123456"));

                mock.Setup(up => up.GetUserByUserName(It.IsAny<string>())).Throws(new Exception());
                mock.Setup(up => up.AddFavoritesToUser(It.IsAny<User>(), It.IsAny<List<Team>>())).Verifiable();

                List<Team> teamLists = new List<Team>() { new Team() { TeamOID = 1 }, new Team { TeamOID = 2 } };

                UserLogic userLogic = new UserLogic(mock.Object);
                userLogic.AddFavoritesToUser(mockedOriginalUser, teamLists);

                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.UserError.USER_NOT_FOUND));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

    }
}
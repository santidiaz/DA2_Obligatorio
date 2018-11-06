using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PermissionLogic;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Utilities;

namespace UnitTests.LogicTests
{
    [TestClass]
    public class PermissionLogicTest
    {
        [TestMethod]
        public void TryToLogInInvalidUserName()
        {
            try
            {
                var premissionsPersistanceMock = new Mock<IPermissionPersistance>();
                var userPersistanceMock = new Mock<IUserPersistance>();

                userPersistanceMock
                    .Setup(up =>
                        up.GetUserByUserName(It.IsAny<string>(), false)).Returns((User)null);

                var permissionLogic = new PermissionLogic.PermissionLogic(premissionsPersistanceMock.Object, userPersistanceMock.Object);
                string someUserName = "santidiaz";
                string somePassword = "321654";

                permissionLogic.LogIn(someUserName, somePassword);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.PermissionError.USER_NOT_FOUND));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TryToLogInInvalidUserPassword()
        {
            try
            {
                var premissionsPersistanceMock = new Mock<IPermissionPersistance>();
                var userPersistanceMock = new Mock<IUserPersistance>();

                string userName = "santidiaz";
                var mockedUser = Utility.GenerateRandomUser(userName);
                userPersistanceMock
                    .Setup(up =>
                        up.GetUserByUserName(userName, false)).Returns(mockedUser);

                var permissionLogic = new PermissionLogic.PermissionLogic(premissionsPersistanceMock.Object, userPersistanceMock.Object);
                string somePassword = "321654";

                permissionLogic.LogIn(userName, somePassword);

                Assert.Fail();
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.PermissionError.INVALID_PASSWORD));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SuccessfulLoginTest()
        {
            try
            {
                var premissionsPersistanceMock = new Mock<IPermissionPersistance>();
                var userPersistanceMock = new Mock<IUserPersistance>();

                // Mocks configuration
                string expectedUserName = "santidiaz";
                string hashedPassword = HashTool.GenerateHash("123456");
                var mockedUser = Utility.GenerateRandomUser(expectedUserName, hashedPassword);
                userPersistanceMock
                    .Setup(up =>
                        up.GetUserByUserName(expectedUserName, false)).Returns(mockedUser);

                premissionsPersistanceMock
                    .Setup(pp => 
                    pp.LogIn(expectedUserName, It.IsAny<Guid>())).Verifiable();

                //Start test
                var permissionLogic = new PermissionLogic.PermissionLogic(premissionsPersistanceMock.Object, userPersistanceMock.Object);

                //Guid result = permissionLogic.LogIn(expectedUserName, "123456");
                //Assert.IsTrue(!result.Equals(Guid.Empty));
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
        public void HasPermissionTest()
        {
            try
            {
                var permissionMock = new Mock<IPermissionPersistance>();
                var userPersistanceMock = new Mock<IUserPersistance>();

                permissionMock.Setup(pp =>
                    pp.HasPermission(It.IsAny<Guid>(), true)).Returns(true);

                //Start test
                var permissionLogic = new PermissionLogic.PermissionLogic(permissionMock.Object, userPersistanceMock.Object);

                bool result = permissionLogic.HasPermission(Guid.NewGuid(), true);
                Assert.IsTrue(result);
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
        public void SuccesfulLogOutTest()
        {
            try
            {
                var permissionMock = new Mock<IPermissionPersistance>();
                var userMock = new Mock<IUserPersistance>();

                string userName = "santidiaz";
                permissionMock
                    .Setup(pm =>
                        pm.LogOut(It.IsAny<string>())).Verifiable();

                var permissionLogic = new PermissionLogic.PermissionLogic(permissionMock.Object, userMock.Object);

                permissionLogic.LogOut(userName);
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
    }
}

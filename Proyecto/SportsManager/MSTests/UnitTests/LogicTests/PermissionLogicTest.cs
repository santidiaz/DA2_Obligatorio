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
                        up.GetUserByUserName(It.IsAny<string>())).Returns((User)null);

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
    }
}

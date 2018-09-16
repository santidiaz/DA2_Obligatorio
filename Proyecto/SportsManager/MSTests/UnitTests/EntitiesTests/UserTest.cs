using System;
using CommonUtilities;
using BusinessEntities;
using UnitTests.Utilities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUser()
        {
            string expectedName = string.Empty;
            string expectedLastName = string.Empty;
            string expectedUserName = string.Empty;
            bool expectedIsAdminFlag = false;
            string expectedEmail = string.Empty;
            List<Team> expectedFavouriteTeams = new List<Team>();

            User user = new User();
            List<Team> actualFavouriteTeams = user.GetFavouritesTeams();

            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedLastName, user.LastName);
            Assert.AreEqual(expectedUserName, user.UserName);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedIsAdminFlag, user.IsAdmin);

            Assert.IsTrue(Utility.CompareLists(actualFavouriteTeams, expectedFavouriteTeams));
        }

        [TestMethod]
        public void CompareUserPassword()
        {
            string expectedCorrectPassword = "123456";
            string expectedWrongPassword = "654321";
            User user = new User { SetPassword = "123456" };
            
            Assert.IsTrue(user.ComparePassword(expectedCorrectPassword));
            Assert.IsFalse(user.ComparePassword(expectedWrongPassword));
        }

        [TestMethod]
        public void CreateUserWithParameters()
        {
            string expectedName = "Santiago";
            string expectedLastName = "Diaz";
            string expectedUserName = "santidiaz";
            bool expectedIsAdminFlag = false;
            string expectedEmail = "santidiaz.uy@gmail.com";
            string expectedPassword = "123456";
            List<Team> expectedFavouriteTeams = new List<Team>();

            User user = new User(expectedName, expectedLastName, expectedUserName, expectedPassword, expectedEmail, expectedIsAdminFlag);

            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedLastName, user.LastName);
            Assert.AreEqual(expectedUserName, user.UserName);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.IsTrue(user.ComparePassword(expectedPassword));
            Assert.AreEqual(expectedIsAdminFlag, user.IsAdmin);

            List<Team> actualFavouritesTeams = user.GetFavouritesTeams();
            Assert.IsTrue(Utility.CompareLists(actualFavouritesTeams, expectedFavouriteTeams));
        }

        [TestMethod]
        public void ThrowExceptionOnInvalidEmailFormat()
        {
            try
            {
                string name = Utility.GetRandomName();
                string lastName = Utility.GetRandomLastName();
                string email = "sd##sdad123"; // Invalid format

                User user = new User();
                user.Email = email;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.INVALID_EMAIL_FORMAT));
            }
        }

        [TestMethod]
        public void ValidateCorrectEmailFormat()
        {
            try
            {
                string name = Utility.GetRandomName();
                string lastName = Utility.GetRandomLastName();
                string email = "santidiaz.uy@gmail.com"; // Invalid format

                User user = new User();
                user.Email = email;
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ValidateRequiredUserName()
        {
            try
            {
                string emptyUserName = string.Empty;
                User user = new User();
                user.UserName = emptyUserName;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.USER_NAME_REQUIRED));
            }
        }

        [TestMethod]
        public void ValidateRequiredPassword()
        {
            try
            {
                string emptyPassword = null;
                User user = new User();
                user.SetPassword = emptyPassword;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.PASSWORD_REQUIRED));
            }
        }

        [TestMethod]
        public void ValidateRequiredName()
        {
            try
            {
                string emptyName = string.Empty;
                User user = new User();
                user.Name = emptyName;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.NAME_REQUIRED));
            }
        }

        [TestMethod]
        public void ValidateRequiredLastName()
        {
            try
            {
                string emptyLastName = "";
                User user = new User();
                user.LastName = emptyLastName;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.LAST_NAME_REQUIRED));
            }
        }
    }
}

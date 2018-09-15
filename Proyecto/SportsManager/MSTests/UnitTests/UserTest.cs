using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UnitTests.Utilities;

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
            string expectedPassword = string.Empty;
            List<Team> expectedFavouriteTeams = new List<Team>();

            User user = new User();
            List<Team> actualFavouriteTeams = user.GetFavouritesTeams();

            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedLastName, user.LastName);
            Assert.AreEqual(expectedUserName, user.UserName);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedPassword, user.Password);
            Assert.AreEqual(expectedIsAdminFlag, user.IsAdmin);

            Assert.IsTrue(Utility.CompareLists(actualFavouriteTeams, expectedFavouriteTeams));
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
            Assert.AreEqual(expectedPassword, user.Password);
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
                Assert.IsTrue(ex.Message.Equals(Constants.Errors.ERROR_INVALID_EMAIL_FORMAT));
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
    }
}

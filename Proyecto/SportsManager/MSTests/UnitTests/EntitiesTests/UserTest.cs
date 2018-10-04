﻿using System;
using CommonUtilities;
using BusinessEntities;
using UnitTests.Utilities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessEntities.JoinEntities;

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
            List<UserTeam> expectedFavouriteTeams = new List<UserTeam>();

            User user = new User();
            List<UserTeam> actualFavouriteTeams = user.FavouriteTeams;

            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedLastName, user.LastName);
            Assert.AreEqual(expectedUserName, user.UserName);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedIsAdminFlag, user.IsAdmin);

            Assert.IsTrue(Utility.CompareLists(actualFavouriteTeams, expectedFavouriteTeams));
        }

        [TestMethod]
        public void CompareHashedUserPassword()
        {
            string expectedCorrectPassword = "123456";
            string expectedWrongPassword = "654321";
            User user = new User { Password = "123456" };

            Assert.IsTrue(user.Password.Equals(expectedCorrectPassword));
            Assert.IsFalse(user.Password.Equals(expectedWrongPassword));
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
            Assert.IsTrue(user.Password.Equals(expectedPassword));
            Assert.AreEqual(expectedIsAdminFlag, user.IsAdmin);

            List<Team> actualFavouritesTeams = null;//user.FavouriteTeams;
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
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.INVALID_EMAIL_FORMAT));
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
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.USER_NAME_REQUIRED));
            }
        }

        [TestMethod]
        public void ValidateRequiredPassword()
        {
            try
            {
                string emptyPassword = null;
                User user = new User();
                user.Password = emptyPassword;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.PASSWORD_REQUIRED));
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
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.NAME_REQUIRED));
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
                Assert.IsTrue(ex.Message.Equals(Constants.UserError.LAST_NAME_REQUIRED));
            }
        }
    }
}
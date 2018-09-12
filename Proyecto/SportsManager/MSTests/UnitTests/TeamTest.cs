using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void CreateTeam()
        {
            var expectedName = "Nacional";
            var expectedPhoto = "Foto1";

            Team team = new Team();
            team.Name = expectedName;
            team.Photo = expectedPhoto;

            Assert.AreEqual(team.Name, expectedName);
            Assert.AreEqual(team.Photo, expectedPhoto);
        }
    }
}

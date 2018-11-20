using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.EntitiesTests
{
    [TestClass]
    public class EventResultTests
    {
        [TestMethod]
        public void GenerateEventResultForMultipleTeams()
        {
            var teamNames = new List<string> { "t1", "t2", "t3", "t4" };
            var isMultipleEventTeams = true;
            var isDrawMatch = false;

            var eventResult = new EventResult(teamNames, isMultipleEventTeams, isDrawMatch);
            Assert.AreEqual(eventResult.TeamsResult.Count, 4);
            eventResult.TeamsResult.ForEach(tp
                => {
                    if (tp.Equals("t1"))
                        Assert.AreEqual(tp.TeamPoints, 3);
                    else if (tp.Equals("t2"))
                        Assert.AreEqual(tp.TeamPoints, 2);
                    else if (tp.Equals("t3"))
                        Assert.AreEqual(tp.TeamPoints, 1);
                    else if (tp.Equals("t4"))
                        Assert.AreEqual(tp.TeamPoints, 0);
                });
        }

        [TestMethod]
        public void GenerateResultForTwoTeamsEvent()
        {
            var teamNames = new List<string> { "t1", "t2" };
            var isMultipleEventTeams = false;
            var isDrawMatch = false;

            var eventResult = new EventResult(teamNames, isMultipleEventTeams, isDrawMatch);
            Assert.AreEqual(eventResult.TeamsResult.Count, 2);
            Assert.AreEqual(eventResult.GetFirst().TeamPoints, 3);
            Assert.AreEqual(eventResult.GetSecond().TeamPoints, 0);
        }

        [TestMethod]
        public void GenerateDrawResultForTwoTeamsEvent()
        {
            var teamNames = new List<string> { "t1", "t2" };
            var isMultipleEventTeams = false;
            var isDrawMatch = true;

            var eventResult = new EventResult(teamNames, isMultipleEventTeams, isDrawMatch);
            Assert.AreEqual(eventResult.GetEventResults().Count, 2);
            Assert.AreEqual(eventResult.TeamsResult[0].TeamPoints, 1);
            Assert.AreEqual(eventResult.TeamsResult[1].TeamPoints, 1);
        }
    }
}

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
    public class CommentLogicTest
    {
        [TestMethod]
        public void AddComment()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
            var mock = new Mock<ICommentPersistance>();
            var mockEvent = new Mock<IEventPersistance>();
            mock.Setup(mr => mr.UserCreatorExists(It.IsAny<string>())).Returns(true);
            mock.Setup(mr => mr.AddComment(It.IsAny<Comment>(), 1)).Verifiable();
            mockEvent.Setup(mr => mr.GetAllEvents()).Returns(new List<Event>() { new Event() { Id = 1 } });


            // Instancio SportLogic con el mock como parametro.
            CommentLogic userLogic = new CommentLogic(mock.Object, mockEvent.Object);
            Comment commentToAdd = new Comment() { Description = "Comment1", CreatorName = "goku" };
            userLogic.AddComment(commentToAdd, 1);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddCommentThatUserCreatorNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
                var mock = new Mock<ICommentPersistance>();
                var mockEvent = new Mock<IEventPersistance>();
                mock.Setup(mr => mr.UserCreatorExists(It.IsAny<string>())).Returns(false);
                mock.Setup(mr => mr.AddComment(It.IsAny<Comment>(), 1)).Verifiable();
                mockEvent.Setup(mr => mr.GetAllEvents()).Returns(new List<Event>() { new Event() { Id = 1 } });

                // Instancio SportLogic con el mock como parametro.
                CommentLogic userLogic = new CommentLogic(mock.Object, mockEvent.Object);
                Comment commentToAdd = new Comment() { Description = "Comment1", CreatorName = "goku" };
                userLogic.AddComment(commentToAdd, 1);
                
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.CommentError.ERROR_CREATOR_NAME_NOT_EXISTS));
            }

        }

        [TestMethod]
        public void AddCommentThatEventNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
                var mock = new Mock<ICommentPersistance>();
                var mockEvent = new Mock<IEventPersistance>();
                mock.Setup(mr => mr.UserCreatorExists(It.IsAny<string>())).Returns(true);
                mock.Setup(mr => mr.AddComment(It.IsAny<Comment>(), 1)).Verifiable();
                mockEvent.Setup(mr => mr.GetAllEvents()).Returns(new List<Event>() { new Event() { Id = 2 } });


                // Instancio SportLogic con el mock como parametro.
                CommentLogic userLogic = new CommentLogic(mock.Object, mockEvent.Object);
                Comment commentToAdd = new Comment() { Description = "Comment1", CreatorName = "goku" };
                userLogic.AddComment(commentToAdd, 1);
                
                Assert.IsTrue(true);
            }
            catch (EntitiesException eEx)
            {
                Assert.IsTrue(eEx.Message.Equals(Constants.EventError.NOT_FOUND));
            }
        }
    }
}

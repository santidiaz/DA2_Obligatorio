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
    public class CommentLogicTest
    {
        [TestMethod]
        public void AddComment()
        {
            // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
            var mock = new Mock<ICommentPersistance>();
            mock.Setup(mr => mr.UserCreatorExists(It.IsAny<string>())).Returns(true);
            mock.Setup(mr => mr.AddComment(It.IsAny<Comment>())).Verifiable();

            // Instancio SportLogic con el mock como parametro.
            CommentLogic userLogic = new CommentLogic(mock.Object);
            Comment commentToAdd = new Comment() { Description = "Comment1", CreatorName = "goku" };
            userLogic.AddComment(commentToAdd);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddCommentThatUserCreatorNotExists()
        {
            try
            {
                // Creo el objeto mock, en este caso una implementacion mockeada de ISportPersistance.
                var mock = new Mock<ICommentPersistance>();
                mock.Setup(mr => mr.UserCreatorExists(It.IsAny<string>())).Returns(false);
                mock.Setup(mr => mr.AddComment(It.IsAny<Comment>())).Verifiable();

                // Instancio SportLogic con el mock como parametro.
                CommentLogic userLogic = new CommentLogic(mock.Object);
                Comment commentToAdd = new Comment() { Description = "Comment1", CreatorName = "goku" };
                userLogic.AddComment(commentToAdd);
                
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.CommentError.ERROR_CREATOR_NAME_NOT_EXISTS));
            }

        }
    }
}

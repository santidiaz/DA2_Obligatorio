using BusinessEntities;
using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.EntitiesTests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void ThrowExceptionOnCreateDescriptionRequired()
        {
            try
            {
                Comment comment = new Comment();
                comment.Description = string.Empty;
                comment.CreatorName = Constants.Comment.CREATORNAME_TEST;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.CommentError.DESCRIPTION_REQUIRED));
            }
        }

        [TestMethod]
        public void ThrowExceptionOnCreateCreatorNameRequired()
        {
            try
            {
                Comment comment = new Comment();
                comment.Description = Constants.Comment.DESCRIPTION_TEST;
                comment.CreatorName = string.Empty;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(Constants.CommentError.CREATORNAME_REQUIRED));
            }
        }

        [TestMethod]
        public void CreateComment()
        {
            var expectedDescription = Constants.Comment.DESCRIPTION_TEST;
            var expectedCreatorName = Constants.Comment.CREATORNAME_TEST;

            Comment comment = new Comment();
            comment.Description  = expectedDescription;
            comment.CreatorName = expectedCreatorName;

            Assert.AreEqual(comment.Description, expectedDescription);
            Assert.AreEqual(comment.CreatorName, expectedCreatorName);
        }

        [TestMethod]
        public void CreateCommentWithParameters()
        {
            var expectedDescription = Constants.Comment.DESCRIPTION_TEST;
            var expectedCreatorName = Constants.Comment.CREATORNAME_TEST;

            Comment comment = new Comment(expectedDescription, expectedCreatorName);

            Assert.AreEqual(expectedDescription, comment.Description);
            Assert.AreEqual(expectedCreatorName, comment.CreatorName);
        }
    }
}

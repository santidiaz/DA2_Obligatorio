using BusinessContracts;
using BusinessEntities;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CommentLogic : ICommentLogic
    {
        private ICommentPersistance persistanceProvider;

        public CommentLogic(ICommentPersistance provider)
        {
            this.persistanceProvider = provider;
        }

        public void AddComment(Comment commentToAdd)
        {
            if (!this.UserCreatorExists(commentToAdd))
                throw new Exception(Constants.CommentError.ERROR_CREATOR_NAME_NOT_EXISTS);
            else
                this.persistanceProvider.AddComment(commentToAdd);
        }

        private bool UserCreatorExists(Comment commentToAdd)
        {
            return this.persistanceProvider.UserCreatorExists(commentToAdd.CreatorName);
        }
    }
}

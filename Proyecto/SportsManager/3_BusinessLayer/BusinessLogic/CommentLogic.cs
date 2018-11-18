using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using CommonUtilities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CommentLogic : ICommentLogic
    {
        private ICommentPersistance commentPersistanceProvider;
        private IEventPersistance eventPersistanceProvider;

        public CommentLogic(ICommentPersistance commentProvider, IEventPersistance eventProvider)
        {
            this.commentPersistanceProvider = commentProvider;
            this.eventPersistanceProvider = eventProvider;
        }

        public void AddComment(Comment commentToAdd, int Id)
        {
            if (!this.UserCreatorExists(commentToAdd))
                throw new EntitiesException(Constants.CommentError.ERROR_CREATOR_NAME_NOT_EXISTS, ExceptionStatusCode.NotFound);
            
            Event foundEvent = eventPersistanceProvider.GetEventById(Id);
            if(foundEvent == null)
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);

            this.commentPersistanceProvider.AddComment(commentToAdd, Id);                
        }

        public bool UserCreatorExists(Comment commentToAdd)
        {
            return this.commentPersistanceProvider.UserCreatorExists(commentToAdd.CreatorName);
        }
    }
}

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

            bool result = false;
            List<Event> events = eventPersistanceProvider.GetAllEvents();
            foreach (var aux in events)
            {
                if (aux.Id==Id) { result = true; };

            }
            if (result)
                this.commentPersistanceProvider.AddComment(commentToAdd, Id);
            else
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);
        }

        public bool UserCreatorExists(Comment commentToAdd)
        {
            return this.commentPersistanceProvider.UserCreatorExists(commentToAdd.CreatorName);
        }
    }
}

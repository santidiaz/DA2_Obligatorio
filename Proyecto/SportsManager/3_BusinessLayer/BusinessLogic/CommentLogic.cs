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
        private ICommentPersistance persistanceProvider;
        private IEventPersistance eventPersistanceProvider;

        public CommentLogic(ICommentPersistance provider, IEventPersistance eventPersistanceProvider)
        {
            this.persistanceProvider = provider;
            this.eventPersistanceProvider = eventPersistanceProvider;
        }

        public void AddComment(Comment commentToAdd, int eventOID)
        {
            if (!this.UserCreatorExists(commentToAdd))
                throw new EntitiesException(Constants.CommentError.ERROR_CREATOR_NAME_NOT_EXISTS, ExceptionStatusCode.NotFound);

            bool result = false;
            List<Event> events = eventPersistanceProvider.GetAllEvents();
            foreach (var aux in events)
            {
                if (aux.EventOID==eventOID) { result = true; };

            }
            if (result)
                this.persistanceProvider.AddComment(commentToAdd, eventOID);
            else
                throw new EntitiesException(Constants.EventError.NOT_FOUND, ExceptionStatusCode.NotFound);
        }

        public bool UserCreatorExists(Comment commentToAdd)
        {
            return this.persistanceProvider.UserCreatorExists(commentToAdd.CreatorName);
        }

    }
}

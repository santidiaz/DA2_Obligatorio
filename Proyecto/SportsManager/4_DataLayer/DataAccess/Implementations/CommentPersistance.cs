﻿using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class CommentPersistance : ICommentPersistance
    {
        public void AddComment(Comment comment, int eventOID)
        {
            using (Context context = new Context())
            {
                Event foundEvent = context.Events.OfType<Event>().Include(e => e.Comments)
                    .FirstOrDefault(e => e.EventOID.Equals(eventOID));

                foundEvent.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public bool UserCreatorExists(string creatorName)
        {
            User foundUser;
            using (Context context = new Context())
            {
                foundUser = context.Users.OfType<User>().FirstOrDefault(u => u.UserName.Equals(creatorName));
            }
            return foundUser != null ? true : false;
        }
    }
}

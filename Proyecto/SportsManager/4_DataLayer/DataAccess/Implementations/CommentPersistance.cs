using BusinessEntities;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Implementations
{
    public class CommentPersistance : ICommentPersistance
    {
        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
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

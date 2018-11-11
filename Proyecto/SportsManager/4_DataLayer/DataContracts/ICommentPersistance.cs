using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace DataContracts
{
    public interface ICommentPersistance
    {
        void AddComment(Comment comment, int Id);
        bool UserCreatorExists(string creatorName);
    }
}

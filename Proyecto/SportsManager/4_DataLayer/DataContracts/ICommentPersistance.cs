using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace DataContracts
{
    public interface ICommentPersistance
    {
        void AddComment(Comment comment);
        bool UserCreatorExists(string creatorName);
    }
}

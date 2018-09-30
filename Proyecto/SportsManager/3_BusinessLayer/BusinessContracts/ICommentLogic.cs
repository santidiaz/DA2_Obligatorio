using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ICommentLogic
    {
        void AddComment(Comment commentToAdd);
        bool UserCreatorExists(Comment commentToAdd);
    }
}

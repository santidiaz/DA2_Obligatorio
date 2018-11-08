using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessContracts
{
    public interface ICommentLogic
    {
        void AddComment(Comment commentToAdd, int Id);
        bool UserCreatorExists(Comment commentToAdd);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Models.CommentModel
{
    public class AddCommentInput
    {
        public string Description { get; set; }
        public string CreatorName { get; set; }
    }
}

using BusinessContracts;
using BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Models.CommentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentLogic commentOperations = Provider.GetInstance.GetCommentOperations();

        [HttpPost()]
        public IActionResult AddComment([FromBody] AddCommentInput addCommentInput)
        {
            try
            {
                if (addCommentInput == null) return BadRequest();

                Comment newComment = new Comment
                {
                    Description = addCommentInput.Description,
                    CreatorName = addCommentInput.CreatorName
                };

                commentOperations.AddComment(newComment, addCommentInput.EventOID);
                return Ok();
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

    }
}

using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models.CommentModel;
using SportsWebApi.Utilities;
using System;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentLogic commentOperations = Provider.GetInstance.GetCommentOperations();

        [PermissionFilter(false)]
        [HttpPost()]
        public IActionResult AddComment([FromBody] AddCommentInput addCommentInput)
        {
            try
            {
                if (addCommentInput == null || 
                    addCommentInput.Id <= 0 ||
                    string.IsNullOrEmpty(addCommentInput.Description)) return BadRequest();

                Comment newComment = new Comment
                {
                    Description = addCommentInput.Description,
                    CreatorName = addCommentInput.CreatorName
                };

                commentOperations.AddComment(newComment, addCommentInput.Id);
                return Ok();
            }
            catch (EntitiesException ex)
            {
                return this.StatusCode(Utility.GetStatusResponse(ex), ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

    }
}

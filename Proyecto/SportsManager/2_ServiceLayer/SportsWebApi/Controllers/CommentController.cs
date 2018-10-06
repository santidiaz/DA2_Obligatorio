using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Models.CommentModel;
using SportsWebApi.Utilities;
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
                if (addCommentInput.EventOID == null || addCommentInput.EventOID <= 0) return BadRequest();
                if (string.IsNullOrEmpty(addCommentInput.Description)) return BadRequest();

                Comment newComment = new Comment
                {
                    Description = addCommentInput.Description,
                    CreatorName = addCommentInput.CreatorName
                };

                commentOperations.AddComment(newComment, addCommentInput.EventOID);
                return Ok();
            }
            catch (EntitiesException ex)
            {
                return this.StatusCode(Utility.GetStatusResponse(ex), ex.Message);
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

    }
}

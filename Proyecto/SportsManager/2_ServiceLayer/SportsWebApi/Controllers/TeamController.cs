using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;

namespace SportsWebApi.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamController : Controller
    {
        // POST: api/team
        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="user">User created client-side</param>
        /// <param name="invitationCode">Invitation Code recieved</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/team/")]
        public IActionResult PostUser(Team newTeam)
        {
            ITeamLogic TeamOperations = Provider.GetInstance.GetTeamOperations();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TeamOperations.AddTeam(newTeam);
                //if (TeamOperations.AddTeam(newTeam))
                    //return Ok(newUser.User);
                //return BadRequest();
            }
            catch (Exception ue)
            {
                return BadRequest(ue.Message);
            }
            return Ok();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

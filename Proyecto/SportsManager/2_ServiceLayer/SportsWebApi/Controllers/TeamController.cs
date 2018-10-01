using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Models.TeamModel;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();

        [HttpGet("{teamName}")]
        public IActionResult GetTeamByUserName(string teamName)
        {
            try
            {
                if (string.IsNullOrEmpty(teamName))
                    return NotFound();

                Team result = teamOperations.GetTeamByName(teamName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: Ver como manejar las exceptions, por ejemplo si es NOT_FOUND de BL
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public IActionResult AddTeam([FromBody] AddTeamInput addTeamInput)
        {
            try
            {
                if (addTeamInput == null) return BadRequest();
                
                Team newTeam = new Team
                {
                    Name = addTeamInput.Name
                };

                //var file = HttpContext.Request.Form.Files.GetFile("image");

                //using (var memoryStream = new MemoryStream())
                //{
                //    file.CopyTo(memoryStream);
                //    newTeam.Photo = memoryStream.ToArray();
                //}
                newTeam.Photo = new byte[5];

                teamOperations.AddTeam(newTeam);
                return Ok();
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userName}")]
        public IActionResult DeleteTeamByUserName(string teamName)
        {
            try
            {
                this.teamOperations.DeleteTeamByName(teamName);
                return Ok();
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        public IActionResult ModifyTeamByName([FromBody] ModifyTeamInput modifyTeamInput)
        {
            try
            {
                if (modifyTeamInput == null) return BadRequest();

                Team modifyTeam = new Team
                {
                    Name = modifyTeamInput.NewName
                };

                var file = HttpContext.Request.Form.Files.GetFile("image");

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    modifyTeam.Photo = memoryStream.ToArray();
                }

                teamOperations.ModifyTeamByName(modifyTeamInput.OldName, modifyTeam);
                return Ok();
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{sportName}/events")]
        public IActionResult GetEventsByTeam(string sportName)
        {
            try
            {
                if (string.IsNullOrEmpty(sportName))
                    return NotFound();

                List<Event> result = teamOperations.GetEventsByTeam(sportName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: Ver como manejar las exceptions, por ejemplo si es NOT_FOUND de BL
                return this.StatusCode(500, ex.Message);
            }
        }

    }
}

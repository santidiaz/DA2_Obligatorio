using System;
using System.Collections.Generic;
using System.IO;
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models.TeamModel;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();

        [PermissionFilter(false)]
        [HttpGet("{teamName}")]
        public IActionResult GetTeamByName(string teamName)
        {
            try
            {
                if (string.IsNullOrEmpty(teamName))
                    return NotFound();

                Team result = teamOperations.GetTeamByName(teamName);
                return Ok(result);
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

        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult AddTeam([FromForm] AddTeamInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                Team newTeam = new Team { Name = input.TeamName };

                var file = HttpContext.Request.Form.Files.GetFile("Image");
                if(file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        newTeam.Photo = memoryStream.ToArray();
                    }
                }

                teamOperations.AddTeam(newTeam, input.SportID);
                return Ok();
            }
            catch (EntitiesException eEx)
            {
                return this.StatusCode(Utility.GetStatusResponse(eEx), eEx.Message);
            }
            catch (Exception ex) 
            {
                return this.StatusCode(500, ex.Message);
            }           
        }

        [PermissionFilter(true)]
        [HttpDelete("{teamName}")]
        public IActionResult DeleteTeamByName(string teamName)
        {
            try
            {
                this.teamOperations.DeleteTeamByName(teamName);
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

        [PermissionFilter(true)]
        [HttpPut()]
        public IActionResult ModifyTeamByName([FromForm] ModifyTeamInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                Team modifyTeam = new Team();
                if (!string.IsNullOrEmpty(input.NewName))
                    modifyTeam.Name = input.NewName;

                var file = HttpContext.Request.Form.Files.GetFile("Image");
                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        modifyTeam.Photo = memoryStream.ToArray();
                    }
                }

                teamOperations.ModifyTeamByName(input.OldName, modifyTeam);
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

        [PermissionFilter(false)]
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
            catch (EntitiesException ex)
            {
                return this.StatusCode(Utility.GetStatusResponse(ex), ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [PermissionFilter(false)]
        [HttpGet()]
        public IActionResult GetTeams(bool orderAsc, string teamName)
        {
            try
            {
                List<Team> result = teamOperations.GetTeams(teamName, orderAsc);
                return Ok(result);
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

using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models.TeamModel;
using SportsWebApi.Utilities;
using System;
using System.Collections.Generic;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();

        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult AddSport([FromBody] AddSportInput input)
        {
            try
            {
                if (input == null) return BadRequest();
                
                Sport newSport = new Sport
                {
                    Name = input.Name,
                    AllowdMultipleTeamsEvents = input.MultipleTeamsAllowed
                };
                
                sportOperations.AddSport(newSport);
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
        [HttpGet("{sportName}")]
        public IActionResult GetSportByName(string sportName)
        {
            try
            {
                if (string.IsNullOrEmpty(sportName))
                    return NotFound();

                Sport result = sportOperations.GetSportByName(sportName);
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
        [HttpDelete("{sportName}")]
        public IActionResult DeleteSportByName(string sportName)
        {
            try
            {
                this.sportOperations.DeleteSportByName(sportName);
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
        public IActionResult ModifySportByName([FromBody] ModifySportInput modifySportInput)
        {
            try
            {
                if (modifySportInput == null) return BadRequest();

                Sport modifySport = new Sport
                {
                    Name = modifySportInput.NewName
                };
                
                sportOperations.ModifySportByName(modifySportInput.OldName, modifySport);
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
        public IActionResult GetEventsBySport(string sportName)
        {
            try
            {
                if (string.IsNullOrEmpty(sportName))
                    return NotFound();

                List<Event> result = sportOperations.GetEventsBySport(sportName);
                return Ok(Utility.TransformEventsToDTOs(result));
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
        [HttpGet("{sportName}/resultTable")]
        public IActionResult GetSportResultTable(string sportName)
        {
            try
            {
                if (string.IsNullOrEmpty(sportName))
                    return NotFound();

                var result = Utility.TransformToSportTable(sportOperations.GetSportResultTable(sportName));
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
        public IActionResult GetSports()
        {
            try
            {
                List<Sport> listSport = sportOperations.GetSports();
                return Ok(listSport);
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
    }
}

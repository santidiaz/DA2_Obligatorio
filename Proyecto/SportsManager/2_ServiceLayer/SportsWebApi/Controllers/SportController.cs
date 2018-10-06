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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();

        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult AddSport([FromBody] AddSportInput addSportInput)
        {
            try
            {
                if (addSportInput == null) return BadRequest();

                Sport newSport = new Sport
                {
                    Name = addSportInput.Name
                };
                
                sportOperations.AddSport(newSport);
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

        [PermissionFilter(false)]
        [HttpGet("{sportName}")]
        public IActionResult GetSportByUserName(string sportName)
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
                // TODO: Ver como manejar las exceptions, por ejemplo si es NOT_FOUND de BL
                return this.StatusCode(500, ex.Message);
            }
        }

        [PermissionFilter(true)]
        [HttpDelete("{sportName}")]
        public IActionResult DeleteSportByUserName(string sportName)
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
            catch (Exception ex)//TODO: Ver como manejar los errores. 
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
            catch (Exception ex)//TODO: Ver como manejar los errores. 
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
                return Ok(result);
            }
            catch (EntitiesException ex)
            {
                return this.StatusCode(Utility.GetStatusResponse(ex), ex.Message);
            }
            catch (Exception ex)
            {
                // TODO: Ver como manejar las exceptions, por ejemplo si es NOT_FOUND de BL
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}

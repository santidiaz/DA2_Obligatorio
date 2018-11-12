using System;
using System.Collections.Generic;
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using FixtureContracts;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using ProviderManager.Helpers;
using SportsWebApi.Filters;
using SportsWebApi.Models.EventModel;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();
        private readonly ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();

        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult Add([FromBody] AddEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                eventOperations.AddEvent(input.SportName, input.TeamNames, input.EventDate);
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                this.eventOperations.DeleteEventById(id);
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
        [HttpPut("{id}")]
        public IActionResult Modify(int id, [FromBody] ModifyEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                this.eventOperations.ModifyEvent(id, input.TeamNames, input.InitialDate);
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

        [PermissionFilter(false)]
        [HttpGet()]
        public IActionResult GetAllEvents()
        {
            try
            {
                var result = Utility.TransformEventsToDTOs(this.eventOperations.GetAllEvents());
                return Ok(result);
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

        [PermissionFilter(false)]
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                var result = Utility.TransformEventToDTO(this.eventOperations.GetEventById(id));
                return Ok(result);
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
        [HttpPut("SetupResult/{id}")]
        public IActionResult SetupResult(int id, [FromBody] EventResultInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                this.eventOperations.SetupEventResult(id, input.TeamNames, input.DrawMatch);
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
    }
}
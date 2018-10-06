using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        private IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();
        private ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();

        [PermissionFilter(true)]
        [HttpGet(nameof(GenerateFixture))]
        public IActionResult GenerateFixture([FromQuery] GenerateFixtureInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                Sport foundSport = sportOperations.GetSportByName(input.SportName);
                IFixture fixtureOption = Provider.GetInstance.GetFixtureGenerator((FixtureType)input.FixtureType);
                List<Event> generatedEvents = fixtureOption.GenerateFixture(foundSport, input.InitialDate);

                return Ok(new StringContent(JArray.FromObject(generatedEvents).ToString(), Encoding.UTF8, "application/json"));
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
        [HttpPost()]
        public IActionResult AddEvent([FromBody] AddEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (input.LocalTeamName.Equals(input.AwayTeamName))
                    return BadRequest("Teams must be different.");

                eventOperations.AddEvent(input.SportName, input.LocalTeamName, input.AwayTeamName, input.EventDate);
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
        [HttpDelete("{eventId}")]
        public IActionResult DeleteEventById(int eventId)
        {
            try
            {
                if (eventId <= 0)
                    return NotFound();

                this.eventOperations.DeleteEventById(eventId);
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
                return Ok(this.eventOperations.GetAllEvents());
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
        [HttpGet("{eventId}")]
        public IActionResult GetEventById(int eventId)
        {
            try
            {
                if (eventId <= 0)
                    return NotFound();

                return Ok(this.eventOperations.GetEventById(eventId));
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
        [HttpPut("{eventId}")]
        public IActionResult ModifyUser(int eventId, [FromBody] ModifyEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                this.eventOperations.ModifyEvent(eventId, input.LocalTeamName, input.AwayTeamName, input.InitialDate);
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
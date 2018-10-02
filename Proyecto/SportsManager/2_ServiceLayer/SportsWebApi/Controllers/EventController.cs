using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using ProviderManager.Helpers;
using SportsWebApi.Models.EventModel;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();

        //private void algo()
        //{
        //    IFixture fixtureGenerationOption = Provider.GetInstance.GetFixtureGenerator(FixtureType.Groups);
        //    var generatedEvents = eventOperations.GenerateFixture(fixtureGenerationOption);
        //}

        [HttpPost()]
        public IActionResult AddEvent([FromBody] AddEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (input.FirstTeamName.Equals(input.SecondTeamName))
                    return BadRequest("Teams must be different.");

                eventOperations.AddEvent(input.SportName, input.FirstTeamName, input.SecondTeamName, input.EventDate);
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

        [HttpDelete("{eventId}")]
        public IActionResult DeleteUserByUserName(int eventId)
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
    }
}
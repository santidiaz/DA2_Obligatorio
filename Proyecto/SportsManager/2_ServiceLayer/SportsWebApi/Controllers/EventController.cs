﻿using System;
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
        private IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();
        private ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();
 
        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult AddEvent([FromBody] AddEventInput input)
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
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public IActionResult ModifyEvent(int eventId, [FromBody] ModifyEventInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                this.eventOperations.ModifyEvent(eventId, input.TeamNames, input.InitialDate);
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
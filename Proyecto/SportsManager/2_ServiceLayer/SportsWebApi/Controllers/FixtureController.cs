﻿using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using FixtureContracts;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models.EventModel;
using SportsWebApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixtureController : ControllerBase
    {
        private readonly IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();
        private readonly ISportLogic sportOperations = Provider.GetInstance.GetSportOperations();
        private readonly IList<IFixture> fixturesAlhorithms = FixtureProvider.Provider.GetInstance.GetFixturesAlgorithms();

        //[HttpGet("{id}/Fixture", Name = "AddExercise")]
        //public IActionResult PostExercise(Guid id, [FromBody]ExerciseModel exercise)
        //{
        //    var newExercise = homeworks.AddExercise(id, ExerciseModel.ToEntity(exercise));
        //    if (newExercise == null)
        //    {
        //        return BadRequest();
        //    }
        //    return CreatedAtRoute("GetExercise", new { id = newExercise.Id }, ExerciseModel.ToModel(newExercise));
        //}


        [PermissionFilter(true)]
        [HttpPost()]
        public IActionResult GenerateFixture([FromBody] GenerateFixtureInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                Sport foundSport = sportOperations.GetSportByName(input.SportName);
                IFixture selectedFixture = this.GetSelectedFixture(input.FixtureName);
                List<Event> generatedEvents = selectedFixture.GenerateFixture(foundSport, input.InitialDate);

                return Ok(generatedEvents);
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
        [HttpGet(Name = "GetFixturesAlgorithms")]
        public IActionResult Get()
        {
            try
            {
                IList<string> fixturesNames = this.fixturesAlhorithms.Select(f => f.GetDescription()).ToList();
                return Ok(fixturesNames);
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

        private IFixture GetSelectedFixture(string fixtureName)
        {
            return this.fixturesAlhorithms.FirstOrDefault(f => f.GetDescription().Equals(fixtureName));
        }
    }
}
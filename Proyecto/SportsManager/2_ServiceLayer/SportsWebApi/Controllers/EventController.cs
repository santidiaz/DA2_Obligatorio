using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using ProviderManager.Helpers;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventLogic eventOperations = Provider.GetInstance.GetEventOperations();

        private void algo()
        {
            IFixture fixtureGenerationOption = Provider.GetInstance.GetFixtureGenerator(FixtureType.Groups);
            var generatedEvents = eventOperations.GenerateFixture(fixtureGenerationOption);
        }

        //[HttpGet("{userName}")]
        //public IActionResult GetUserByUserName(string userName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(userName))
        //            return NotFound();

        //        User searchedUser = userOperations.GetUserByUserName(userName);
        //        if (searchedUser == null)
        //            return NotFound();

        //        return Ok(searchedUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.StatusCode(500, ex.Message);
        //    }
        //}
    }
}
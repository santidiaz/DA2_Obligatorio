using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Models;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userOperations = Provider.GetInstance.GetUserOperations();

        // GET api/values/5
        [HttpGet("{userName}")]
        public User GetUserByUserName(string userName)
        {            
            User result = userOperations.GetUserByUserName(userName);
            return result;
        }

        [HttpPost()]
        public IActionResult AddUser([FromBody] AddUserInput addUserInput)
        {
            if (addUserInput == null) return BadRequest();

            User newUser = new User();

            //var city = userOperations.AddUser();

            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var maxPointOfInterestId =
            //    CityDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfInterestId,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //city.PointsOfInterest.Add(finalPointOfInterest);

            //return CreatedAtRoute(
            //    "GetPointOfInterest",
            //    new { cityId, id = finalPointOfInterest.Id },
            //    finalPointOfInterest);
            return Ok();
        }
    }
}
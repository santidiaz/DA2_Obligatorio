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

        [HttpGet("{userName}")]
        public IActionResult GetUserByUserName(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();

                User result = userOperations.GetUserByUserName(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: Ver como manejar las exceptions, por ejemplo si es NOT_FOUND de BL
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public IActionResult AddUser([FromBody] AddUserInput addUserInput)
        {
            try
            {
                if (addUserInput == null) return BadRequest();

                User newUser = new User
                {
                    Email = addUserInput.Email,
                    Name = addUserInput.Name,
                    LastName = addUserInput.LastName,
                    SetPassword = addUserInput.Password,
                    UserName = addUserInput.UserName
                };

                userOperations.AddUser(newUser);
                return Ok();
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}
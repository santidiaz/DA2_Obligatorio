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

                User searchedUser = userOperations.GetUserByUserName(userName);
                if (searchedUser == null)
                    return NotFound();

                return Ok(searchedUser);
            }
            catch (Exception ex)
            {
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
                    Password = addUserInput.Password,
                    UserName = addUserInput.UserName,
                    IsAdmin = addUserInput.IsAdmin
                };

                userOperations.AddUser(newUser);
                return Ok();//201 Created
            }
            catch (Exception ex)//TODO: Ver como manejar los errores. 
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userName}")]
        public IActionResult DeleteUserByUserName(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();

                if (this.userOperations.DeleteUserByUserName(userName))
                    return Ok();
                
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}
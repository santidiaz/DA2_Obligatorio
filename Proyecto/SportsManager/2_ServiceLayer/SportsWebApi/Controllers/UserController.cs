using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userOperations = Provider.GetInstance.GetUserOperations();

        [PermissionFilter(true)]
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

        [HttpPut("{userName}")]
        public IActionResult ModifyUser(string userName,
            [FromBody] ModifyUserInput modyUserInput)
        {
            try
            {
                if (modyUserInput == null)
                    return BadRequest();

                User userModifications = new User
                {
                    UserName = userName,
                    Name = modyUserInput.Name,
                    LastName = modyUserInput.LastName,
                    Email = modyUserInput.Email,
                    IsAdmin = modyUserInput.IsAdmin,
                    Password = modyUserInput.Password,
                };

                bool modificationResponse = this.userOperations.ModifyUser(userModifications);

                if (!modificationResponse)
                    return Accepted();// No modifications were made.

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
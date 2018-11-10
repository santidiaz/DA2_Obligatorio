using System;
using System.Collections.Generic;
using BusinessContracts;
using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessEntities.JoinEntities;
using Microsoft.AspNetCore.Mvc;
using ProviderManager;
using SportsWebApi.Filters;
using SportsWebApi.Models;
using SportsWebApi.Models.UserModel;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userOperations = Provider.GetInstance.GetUserOperations();
        private ITeamLogic teamOperations = Provider.GetInstance.GetTeamOperations();

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
            catch (EntitiesException eEx)
            {
                return this.StatusCode(Utility.GetStatusResponse(eEx), eEx.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        //[PermissionFilter(true)]
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

        //[PermissionFilter(true)]
        [HttpDelete("{userName}")]
        public IActionResult DeleteUserByUserName(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();

                this.userOperations.DeleteUserByUserName(userName);
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
        [HttpPut("{userName}")]
        public IActionResult ModifyUser(string userName, [FromBody] ModifyUserInput modyUserInput)
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

                this.userOperations.ModifyUser(userModifications);
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
        [HttpPost]
        [Route("{userName}/addfavorites")]
        public IActionResult AddFavoritesToUser([FromBody]AddFavoritesToUser app)
        {
            try
            {
                User user = this.userOperations.GetUserByUserName(app.UserName);
                List<Team> listTeans = new List<Team>();

                foreach (var item in app.NetFavouriteTeamsOID)
                {
                    listTeans.Add(teamOperations.GetTeamByOID(item));
                }
                this.userOperations.AddFavoritesToUser(user, listTeans);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(404, ex.Message);
            }
        }

        [PermissionFilter(false)]
        [HttpPut(nameof(ModifyUserFavouriteTeams))]
        public IActionResult ModifyUserFavouriteTeams([FromBody] ModifyUserFavouriteTeamsInput input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                
                this.userOperations.ModifyUserFavouriteTeams(input.UserName, input.TeamNames);
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
        [HttpGet("{userName}/favorites")]
        public IActionResult GetFavoritesTeamsByUserName(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();

                List<UserTeam> searchedUser = userOperations.GetFavoritesTeamsByUserName(userName);
                if (searchedUser == null)
                    return NotFound();

                return Ok(searchedUser);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [PermissionFilter(false)]
        [HttpDelete("{userName}/favorite/{teamOID}")]
        public IActionResult DeleteFavoriteTeamByUser(string userName, int teamOID)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();
                if (teamOID <= 0)
                    return NotFound();

                this.userOperations.DeleteFavoriteTeamByUser(teamOID, userName);
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
        [HttpGet("{userName}/favoriteTeamComments")]
        public IActionResult GetCommentsOfUserFavouriteTemasEvents(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return NotFound();

                List<Event> searchedEvents = userOperations.GetCommentsOfUserFavouriteTemasEvents(userName);
                if (searchedEvents == null)
                    return NotFound();

                return Ok(searchedEvents);
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

        //[PermissionFilter(true)]
        [HttpGet()]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> listUsers = userOperations.GetUsers();
                return Ok(listUsers);
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
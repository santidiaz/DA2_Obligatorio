using System;
using BusinessEntities.Exceptions;
using LogContracts;
using Microsoft.AspNetCore.Mvc;
using PermissionContracts;
using ProviderManager;
using SportsWebApi.Models.AuthenticationModel;
using SportsWebApi.Utilities;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IPermissionLogic permissionOperations = Provider.GetInstance.GetPermissionOperations();
        private readonly ILogger logger = LogProvider.Logger.GetInstance.LogTool();

        [HttpPost(nameof(LogIn))]
        public IActionResult LogIn([FromBody] LogInInput input)
        {
            try
            {
                if (input == null) return BadRequest();

                var sessionData = permissionOperations.LogIn(input.UserName, input.Password);
                var session = new SessionDTO
                {
                    Token = sessionData.Item1,
                    UserName = sessionData.Item2,
                    IsAdmin = sessionData.Item3
                };
                
                logger.LogAction(nameof(LogIn), session.UserName, DateTime.Now);
                return Ok(session);
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

        [HttpPost(nameof(LogOut))]
        public IActionResult LogOut([FromBody] LogOutInput input)
        {
            try
            {
                if (input == null) return BadRequest();

                permissionOperations.LogOut(input.UserName);
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
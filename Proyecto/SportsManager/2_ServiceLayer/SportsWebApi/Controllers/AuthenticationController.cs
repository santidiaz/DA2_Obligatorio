using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Exceptions;
using Microsoft.AspNetCore.Http;
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

        [HttpPost()]
        public IActionResult LogIn([FromBody] LogInInput input)
        {
            try
            {
                if (input == null) return BadRequest();

                Guid token = permissionOperations.LogIn(input.UserName, input.Password);
                return Ok(token);
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
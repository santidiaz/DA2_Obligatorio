using System;
using LogContracts;
using Microsoft.AspNetCore.Mvc;
using SportsWebApi.Filters;

namespace SportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger logger = LogProvider.Logger.GetInstance.LogTool();

        [PermissionFilter(true)]
        [HttpGet()]
        public IActionResult GetLogs(/*agregr fechas desde-hasta*/)
        {
            var result = logger.GetLogs(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

            return Ok();
        }
    }
}

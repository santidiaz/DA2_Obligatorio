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
        public IActionResult GetLogs([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                if (fromDate == null || toDate == null)
                    return BadRequest();

                return Ok(logger.GetLogs(fromDate, toDate));
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}

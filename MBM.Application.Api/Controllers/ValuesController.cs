using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MBM.Application.Api.Controllers
{
    /// <summary>
    /// ValuesController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>
        /// Enity
        /// </returns>
        /// <remarks>
        /// remarks
        /// </remarks>
        /// <response code="201">Returns item</response>
        /// <response code="400">If the item not found</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(string[]), 400)]
        public ActionResult<IEnumerable<string>> Get()
        {
            this.logger.LogTrace("Trace", null);
            this.logger.LogDebug("Debug", null);
            this.logger.LogInformation("Info", null);
            this.logger.LogWarning("Warn", null);
            this.logger.LogError("Error", null);
            this.logger.LogCritical("Fatal", null);

            return new JsonResult(
                new string[] { "AA", "BB" });
        }
    }
}
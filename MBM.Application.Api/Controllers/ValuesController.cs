using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult(
                new string[] { "AA", "BB" });
        }
    }
}
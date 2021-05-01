using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRDBIntegrationService.Contract;

namespace PRDBIntegrationWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ParkingRightRegistrationController : ControllerBase
    {
        /// <summary>
        ///     This is the service that handles external integrations with PRDB Central System
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParkingRightRegistrationRequest request)
        {
            var random = new Random();
            // Publish ParkingRightRegisteredTopic for subscribers
            return StatusCode((int) HttpStatusCode.OK, random.Next(10,10000));
        }
    }
}
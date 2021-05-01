using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingRight.Domain;
using ParkingRight.Domain.Models;

namespace ParkingRight.WebApi.Controllers
{
    [Route("[controller]")]
    public class ParkingRightController : ControllerBase
    {
        private readonly IParkingRightProcessor _parkingRightProcessor;

        public ParkingRightController(IParkingRightProcessor parkingRightProcessor)
        {
            _parkingRightProcessor = parkingRightProcessor;
        }

        /// <summary>
        /// Retrieve the parking-right with the given key. 
        /// </summary>
        /// <param name="parkingRightKey"></param>
        /// <returns></returns>
        [HttpGet("{parkingRightKey}")]
        [ProducesResponseType(typeof(ParkingRightModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ParkingRightModel), (int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<ParkingRightModel>> Get(string parkingRightKey)
        {
            var parkingRightModel = await _parkingRightProcessor.GetParkingRight(parkingRightKey);
            return parkingRightModel == null
                ? StatusCode((int) HttpStatusCode.NotFound, null)
                : StatusCode((int) HttpStatusCode.Accepted, parkingRightModel);
        }

        /// <summary>
        ///     Creates a new parking right
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ParkingRightModel), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ParkingRightModel), (int) HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] ParkingRightModel request)
        {
            var parkingRightModel = await _parkingRightProcessor.SaveParkingRight(request);
            return StatusCode((int) HttpStatusCode.Created, parkingRightModel);
        }

        /// <summary>
        ///  Update parking right with the given key
        /// </summary>
        /// <param name="parkingRightKey"></param>
        /// <param name="value"></param>
        [HttpPut("{parkingRightKey}")]
        public void Put(string parkingRightKey, [FromBody] ParkingRightModel value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The parking right with the given key will be cancelled.
        /// </summary>
        /// <param name="parkingRightKey"></param>
        [HttpDelete("{parkingRightKey}")]
        public void Delete(string parkingRightKey)
        {
            throw new NotImplementedException();
        }
    }
}
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

        // GET parkingright/key
        [HttpGet("{key}")]
        public async Task<ActionResult<ParkingRightModel>> Get(string key)
        {
            var parkingRightModel = await _parkingRightProcessor.GetParkingRight(key);
            return parkingRightModel == null
                ? StatusCode((int) HttpStatusCode.NoContent, null)
                : StatusCode((int) HttpStatusCode.Accepted, parkingRightModel);
        }

        // POST values
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

        //// PUT values/5
        //[HttpPut("{parkingRightKey}")]
        //public void Put(string parkingRightKey, [FromBody]string value)
        //{
        //}

        // DELETE api/values/parkingRightKey
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
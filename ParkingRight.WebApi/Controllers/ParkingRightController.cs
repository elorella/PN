using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingRight.Domain;
using ParkingRight.Domain.Models;

namespace ParkingRight.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ParkingRightController : ControllerBase
    {
        private readonly IParkingRightProcessor _parkingRightProcessor;

        public ParkingRightController(IParkingRightProcessor parkingRightProcessor)
        {
            _parkingRightProcessor = parkingRightProcessor;
        }

        // GET parkingright/key
        [HttpGet("{key}")]
        [ProducesResponseType(typeof(ParkingRightDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ParkingRightDto), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string key)
        {
            var result = await _parkingRightProcessor.GetParkingRight(key);

            return result.IsSuccess
                ? StatusCode((int) HttpStatusCode.OK, result)
                : StatusCode((int) HttpStatusCode.NotFound, null);
        }

        // POST values
        /// <summary>
        ///     Creates a new parking right
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiServiceResult<string>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiServiceResult<string>), (int) HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] ParkingRightInsertRequest request)
        {
            var key = await _parkingRightProcessor.SaveParkingRight(request);

            return key.IsSuccess
                ? StatusCode((int) HttpStatusCode.Created, key)
                : StatusCode((int) HttpStatusCode.UnprocessableEntity, key);
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
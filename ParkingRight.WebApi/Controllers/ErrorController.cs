using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ParkingRight.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("error")]
        public IActionResult Error()
        {
            //Error logging
            return Problem();
        }
    }
}
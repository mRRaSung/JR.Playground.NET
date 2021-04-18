using API.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    public class GlobalController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public GlobalController(IHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        [Route("/errors")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Errors()
        {
            var rspFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var statusCode = rspFeature.Error.GetType().Name switch
            {
                nameof(P4pException) => HttpStatusCode.BadRequest,
                nameof(ArgumentException) => HttpStatusCode.BadRequest,
                nameof(KeyNotFoundException) => HttpStatusCode.NotFound,
                _ => HttpStatusCode.ServiceUnavailable
            };

            if (_env.IsDevelopment())
                return Problem(statusCode: (int)statusCode, title: rspFeature.Error.Message, detail: rspFeature.Error.StackTrace);

            return Problem(statusCode: (int)statusCode, title: rspFeature.Error.Message);
        }
    }
}

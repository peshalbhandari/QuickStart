using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuickStart.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

       public object Get()
        {
            var responseObj = new
            {
                Status="Up"
            };
            _logger.LogInformation($"Status Pinged:{responseObj.Status}");
            return responseObj;
        }
    }
}
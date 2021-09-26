using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ms_sena.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnvironmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<EnvironmentController> _logger;

        public EnvironmentController(ILogger<EnvironmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult>  GetConfig([FromQuery(Name = "conf")] string conf)
        {
            var rng = new Random();
            var result = await Task.FromResult<string>(_configuration[conf]);
            return Ok(result);
        }
    }
}

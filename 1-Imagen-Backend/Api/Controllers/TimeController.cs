using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ms_sena.Infrastructure;

namespace Ms_sena.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<TimeController> _logger;

        private readonly IDataBase _dataBase;

        public TimeController(ILogger<TimeController> logger, IConfiguration configuration, IDataBase dataBase)
        {
            _logger = logger;
            _configuration = configuration;
            _dataBase = dataBase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rng = new Random();
            var result = await Task.FromResult<TimeDto>( new TimeDto
            {
                Date = DateTime.Now,
                Number = rng.Next(0, 55)
            });
            return Ok(result);
        }
        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dataBase.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TimeDto time)
        {
            var result = await _dataBase.AddTimeAsync(time);
            return Ok(result);
        }

    }
}

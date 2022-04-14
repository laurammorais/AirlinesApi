using System.Collections.Generic;
using AirlinesLogApi.Models;
using AirlinesLogApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesLogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;
        public LogController(LogService logService)
        {
            _logService = logService;
        }

        // GET: api/<LogController>
        [HttpGet]
        public ActionResult<IEnumerable<Log>> Get() => _logService.Get();

        // GET api/<LogController>/5
        [HttpGet("{id}")]
        public ActionResult<Log> Get(string id)
        {
            var log = _logService.Get(id);
            if (log == null)
                return NotFound();
            return log;
        }

        // POST api/<LogController>
        [HttpPost]
        public ActionResult<Log> Post([FromBody] Log log) => _logService.Create(log);
    }
}

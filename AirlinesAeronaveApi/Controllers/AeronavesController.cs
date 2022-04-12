using System.Collections.Generic;
using AirlinesAeronaveApi.Models;
using AirlinesAeronaveApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesAeronaveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronavesController : ControllerBase
    {
        private readonly AeronaveService _aeronaveService;
        public AeronavesController(AeronaveService aeronaveService)
        {
            _aeronaveService = aeronaveService;
        }

        // GET: api/<AeronaveController>
        [HttpGet]
        public ActionResult<List<Aeronave>> Get() => _aeronaveService.Get();

        // GET api/<AeronaveController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult<Aeronave> Get(string id)
        {
            var aeronave = _aeronaveService.Get(id);
            if (aeronave == null)
                return NotFound();
            return aeronave;
        }

        // POST api/<AeronaveController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult<Aeronave> Post([FromBody] Aeronave aeronave) => _aeronaveService.Create(aeronave);

        // PUT api/<AeronaveController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Put(string id, [FromBody] Aeronave aeronave)
        {
            var aeronaveGet = _aeronaveService.Get(id);
            if (aeronaveGet == null)
                return NotFound(id);
            _aeronaveService.Update(id, aeronave);
            return Ok();
        }

        // DELETE api/<AeronaveController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            var aeronave = _aeronaveService.Get(id);
            if (aeronave == null)
                return NotFound(id);
            _aeronaveService.Remove(id);
            return Ok();
        }
    }
}
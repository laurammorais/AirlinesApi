using System;
using System.Collections.Generic;
using AirlinesAeronaveApi.Models;
using AirlinesAeronaveApi.Producers;
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
            LogProducer.AddLog("Buscando aeronave.");

            var aeronave = _aeronaveService.Get(id);
            if (aeronave == null)
            {
                LogProducer.AddLog("Aeronave não encontrada.");
                return NotFound();
            }

            LogProducer.AddLog("Aeronave encontrada com sucesso!");
            return aeronave;
        }

        // POST api/<AeronaveController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult<Aeronave> Post([FromBody] Aeronave aeronave)
        {
            try
            {
                LogProducer.AddLog("Criando nova aeronave.");

                aeronave = _aeronaveService.Create(aeronave);

                LogProducer.AddLog("Aeronave criada com sucesso!");

                return aeronave;
            }
            catch (Exception ex)
            {
                LogProducer.AddLog(ex.Message);
                throw;
            }
        }

        // PUT api/<AeronaveController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Put(string id, [FromBody] Aeronave aeronave)
        {
            LogProducer.AddLog("Alterando aeronave.");

            var aeronaveGet = _aeronaveService.Get(id);
            if (aeronaveGet == null)
            {
                LogProducer.AddLog("Aeronave não localizada.");
                return NotFound(id);
            }
            _aeronaveService.Update(id, aeronave);

            LogProducer.AddLog("Aeronave alterada com sucesso!");

            return Ok();
        }

        // DELETE api/<AeronaveController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo aeronave.");

            var aeronave = _aeronaveService.Get(id);
            if (aeronave == null)
            {
                LogProducer.AddLog("Aeronave não localizada!");
                return NotFound(id);
            }

            _aeronaveService.Remove(id);
            LogProducer.AddLog("Aeronave removida com sucesso!");
            return Ok();
        }
    }
}
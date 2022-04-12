using System.Collections.Generic;
using AirlinesVooApi.Models;
using AirlinesVooApi.ModelsInput;
using AirlinesVooApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesVooApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooController : ControllerBase
    {
        private readonly VooService _vooService;
        private readonly AeroportoService _aeroportoService;
        private readonly AeronaveService _aeronaveService;
        public VooController(VooService vooService, AeroportoService aeroportoService, AeronaveService aeronaveService)
        {
            _vooService = vooService;
            _aeroportoService = aeroportoService;
            _aeronaveService = aeronaveService;
        }

        // GET: api/<VooController>
        [HttpGet]
        public ActionResult<IEnumerable<Voo>> Get() => _vooService.Get();

        // GET api/<VooController>/5
        [HttpGet("{id}")]
        public ActionResult<Voo> Get(string id)
        {
            var voo = _vooService.Get(id);
            if (voo == null)
                return NotFound();
            return voo;
        }

        // POST api/<VooController>
        [HttpPost]
        public ActionResult<Voo> Post([FromBody] VooInput vooInput)
        {
            var origem = _aeroportoService.Get(vooInput.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(vooInput.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não cadastrados em nossa base de dados");

            var aeronave = _aeronaveService.Get(vooInput.AeronaveId);
            if (aeronave == null)
                return NotFound("Aeronave não cadastrada em nossa base de dados");

            return _vooService.Create(new Voo
            {
                Origem = origem,
                Destino = destino,
                HorarioDesembarque = vooInput.HorarioDesembarque,
                HorarioEmbarque = vooInput.HorarioEmbarque,
                Aeronave = aeronave
            });
        }

        // PUT api/<VooController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] VooInput vooInput)
        {
            var voo = _vooService.Get(id);

            if (voo == null)
                return NotFound();

            var origem = _aeroportoService.Get(vooInput.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(vooInput.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não cadastrados em nossa base de dados");

            var aeronave = _aeronaveService.Get(vooInput.AeronaveId);
            if (aeronave == null)
                return NotFound("Aeronave não cadastrada em nossa base de dados");

            _vooService.Update(id, new Voo
            {
                Origem = origem,
                Destino = destino,
                HorarioDesembarque = vooInput.HorarioDesembarque,
                HorarioEmbarque = vooInput.HorarioEmbarque,
                Aeronave = aeronave
            });

            return Ok();
        }

        // DELETE api/<VooController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var vooGet = _vooService.Get(id);
            if (vooGet == null)
                return NotFound();
            _vooService.Remove(id);
            return Ok();
        }
    }
}
using System.Collections.Generic;
using AirlinesVooApi.Models;
using AirlinesVooApi.ModelsInput;
using AirlinesVooApi.Producers;
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
            LogProducer.AddLog("Buscando Voo.");

            var voo = _vooService.Get(id);
            if (voo == null)
            {
                LogProducer.AddLog("Voo não encontrado.");
                return NotFound();
            }

            LogProducer.AddLog("Voo encontrado com sucesso!");
            return voo;
        }

        // POST api/<VooController>
        [HttpPost]
        public ActionResult<Voo> Post([FromBody] VooInput vooInput)
        {
            LogProducer.AddLog("Criando novo Voo.");

            var origem = _aeroportoService.Get(vooInput.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(vooInput.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não cadastrados em nossa base de dados");

            var aeronave = _aeronaveService.Get(vooInput.AeronaveId);
            if (aeronave == null)
                return NotFound("Aeronave não cadastrada em nossa base de dados");

            var voo = _vooService.Create(new Voo
            {
                Origem = origem,
                Destino = destino,
                HorarioDesembarque = vooInput.HorarioDesembarque,
                HorarioEmbarque = vooInput.HorarioEmbarque,
                Aeronave = aeronave
            });

            LogProducer.AddLog("Voo criado com sucesso!");
            return voo;
        }

        // PUT api/<VooController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] VooInput vooInput)
        {
            LogProducer.AddLog("Alterando Voo.");

            var voo = _vooService.Get(id);

            if (voo == null)
            {
                LogProducer.AddLog("Voo não localizado.");
                return NotFound();
            }

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

            LogProducer.AddLog("Voo alterado com sucesso!");
            return Ok();
        }

        // DELETE api/<VooController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo Voo.");
            
            var vooGet = _vooService.Get(id);
            if (vooGet == null)
            {
                LogProducer.AddLog("Voo não localizado!");
                return NotFound();
            }
            _vooService.Remove(id);
            LogProducer.AddLog("Voo removido com sucesso!");
            return Ok();
        }
    }
}
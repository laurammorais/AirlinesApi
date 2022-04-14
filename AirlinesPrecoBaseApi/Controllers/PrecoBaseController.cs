using System.Collections.Generic;
using AirlinesPrecoBaseApi.Models;
using AirlinesPrecoBaseApi.ModelsInput;
using AirlinesPrecoBaseApi.Producers;
using AirlinesPrecoBaseApi.Sevices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesPrecoBaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoBaseController : ControllerBase
    {
        private readonly PrecoBaseService _precoBaseService;
        private readonly AeroportoService _aeroportoService;
        public PrecoBaseController(PrecoBaseService precoBaseService, AeroportoService aeroportoService)
        {
            _precoBaseService = precoBaseService;
            _aeroportoService = aeroportoService;
        }

        // GET: api/<PrecoBaseController>
        [HttpGet]
        public ActionResult<IEnumerable<PrecoBase>> Get() => _precoBaseService.Get();

        // GET api/<PrecoBaseController>/5
        [HttpGet("{id}")]
        public ActionResult<PrecoBase> Get(string id)
        {
            LogProducer.AddLog("Buscando Preço Base.");

            var precoBase = _precoBaseService.Get(id);
            if (precoBase == null)
            {
                LogProducer.AddLog("Preço Base não encontrado.");
                return NotFound();
            }

            LogProducer.AddLog("Preço Base encontrado com sucesso!");
            return precoBase;
        }

        // POST api/<PrecoBaseController>
        [HttpPost]
        public ActionResult<PrecoBase> Post([FromBody] PrecoBaseInput precoBase)
        {
            LogProducer.AddLog("Criando novo Preço Base.");

            var origem = _aeroportoService.Get(precoBase.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não existe cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(precoBase.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não existe cadastrados em nossa base de dados");

            var preco = _precoBaseService.Create(new PrecoBase { Origem = origem, Destino = destino, Valor = precoBase.Valor });
            LogProducer.AddLog("Preço Base criado com sucesso!");
            return preco;
        }

        // PUT api/<PrecoBaseController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] PrecoBaseInput precoBaseInput)
        {
            LogProducer.AddLog("Alterando Preço Base.");

            var precoBaseGet = _precoBaseService.Get(id);
            if (precoBaseGet == null)
            {
                LogProducer.AddLog("Preço Base não localizado.");
                return NotFound();
            }

            var origem = _aeroportoService.Get(precoBaseInput.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não existe cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(precoBaseInput.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não existe cadastrados em nossa base de dados");

            _precoBaseService.Update(id, new PrecoBase { Origem = origem, Destino = destino, Valor = precoBaseInput.Valor });
            LogProducer.AddLog("Preço Base alterado com sucesso!");
            return Ok();
        }

        // DELETE api/<PrecoBaseController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo Preço Base.");

            var precoBase = _precoBaseService.Get(id);
            if (precoBase == null)
            {
                LogProducer.AddLog("Preço Base não localizado!");
                return NotFound();
            }
            _precoBaseService.Remove(id);
            LogProducer.AddLog("Preço Base removido com sucesso!");
            return Ok();
        }
    }
}
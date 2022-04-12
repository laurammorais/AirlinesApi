using System.Collections.Generic;
using AirlinesPrecoBaseApi.Models;
using AirlinesPrecoBaseApi.ModelsInput;
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
            var precoBase = _precoBaseService.Get(id);
            if (precoBase == null)
                return NotFound();
            return precoBase;
        }

        // POST api/<PrecoBaseController>
        [HttpPost]
        public ActionResult<PrecoBase> Post([FromBody] PrecoBaseInput precoBase)
        {
            var origem = _aeroportoService.Get(precoBase.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não existe cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(precoBase.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não existe cadastrados em nossa base de dados");

            return _precoBaseService.Create(new PrecoBase { Origem = origem, Destino = destino, Valor = precoBase.Valor });
        }

        // PUT api/<PrecoBaseController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] PrecoBaseInput precoBaseInput)
        {
            var precoBaseGet = _precoBaseService.Get(id);
            if (precoBaseGet == null)
                return NotFound();

            var origem = _aeroportoService.Get(precoBaseInput.OrigemSigla);
            if (origem == null)
                return NotFound("Aeroporto de Origem não existe cadastrados em nossa base de dados");

            var destino = _aeroportoService.Get(precoBaseInput.DestinoSigla);
            if (destino == null)
                return NotFound("Aeroporto de Destino não existe cadastrados em nossa base de dados");

            _precoBaseService.Update(id, new PrecoBase { Origem = origem, Destino = destino, Valor = precoBaseInput.Valor });
            return Ok();
        }

        // DELETE api/<PrecoBaseController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var precoBase = _precoBaseService.Get(id);
            if (precoBase == null)
                return NotFound();
            _precoBaseService.Remove(id);
            return Ok();
        }
    }
}
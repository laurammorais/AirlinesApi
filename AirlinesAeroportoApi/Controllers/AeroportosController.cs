using System.Collections.Generic;
using System.Threading.Tasks;
using AirlinesAeroportoApi.Models;
using AirlinesAeroportoApi.ModelsInput;
using AirlinesAeroportoApi.Producers;
using AirlinesAeroportoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesAeroportoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportosController : ControllerBase
    {
        private readonly AeroportoService _aeroportoService;
        public AeroportosController(AeroportoService aeroportoService)
        {
            _aeroportoService = aeroportoService;
        }

        // GET: api/<AeroportosController>
        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult<IEnumerable<Aeroporto>> Get() => _aeroportoService.Get();

        // GET api/<AeroportosController>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Aeroporto> Get(string id)
        {
            LogProducer.AddLog("Buscando aeroporto.");

            var aeroporto = _aeroportoService.Get(id);
            if (aeroporto == null)
            {
                LogProducer.AddLog("Aeroporto não encontrado.");
                return NotFound();
            }
           
            LogProducer.AddLog("Aeroporto encontrado com sucesso!");
            return aeroporto;
        }

        // POST api/<AeroportosController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Aeroporto>> Post([FromBody] AeroportoInput aeroportoInput)
        {
            LogProducer.AddLog("Criando novo Aeroporto.");

            var endereco = await ViaCepService.GetEndereco(aeroportoInput.Endereco.Cep);

            if (endereco == null)
            {
                LogProducer.AddLog("Cep não localizado!");
                return BadRequest("Cep Inválido!");
            }

            endereco.Numero = aeroportoInput.Endereco.Numero;
            endereco.Complemento = aeroportoInput.Endereco.Complemento;
            var aeroporto = _aeroportoService.Create(new Aeroporto { Nome = aeroportoInput.Nome, Sigla = aeroportoInput.Sigla, Endereco = endereco });
            LogProducer.AddLog("Aeroporto criado com sucesso!");
            return aeroporto;
        }


        // PUT api/<AeroportosController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Put(string id, [FromBody] AeroportoInput aeroportoInput)
        {
            LogProducer.AddLog("Alterando Aeroporto.");

            var aeroportoGet = _aeroportoService.Get(id);

            if (aeroportoGet == null)
            {
                LogProducer.AddLog("Aeroporto não localizado.");
                return NotFound(id);
            }

            var endereco = await ViaCepService.GetEndereco(aeroportoInput.Endereco.Cep);

            if (endereco == null)
            {
                LogProducer.AddLog("Cep não localizado!");
                return BadRequest("Cep Inválido!");
            }

            endereco.Numero = aeroportoInput.Endereco.Numero;
            endereco.Complemento = aeroportoInput.Endereco.Complemento;

            _aeroportoService.Update(id, new Aeroporto { Nome = aeroportoInput.Nome, Sigla = aeroportoInput.Sigla, Endereco = endereco });
            LogProducer.AddLog("Aeroporto alterado com sucesso!");
            return Ok();
        }

        // DELETE api/<AeroportosController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo Aeroporto.");

            var aeroporto = _aeroportoService.Get(id);
            if (aeroporto == null)
            {
                LogProducer.AddLog("Aeroporto não localizado!");
                return NotFound();
            }
            _aeroportoService.Remove(id);
            LogProducer.AddLog("Aeroporto removido com sucesso!");
            return Ok();
        }
    }
}
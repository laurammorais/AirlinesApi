using System.Collections.Generic;
using System.Threading.Tasks;
using AirlinesAeroportoApi.Models;
using AirlinesAeroportoApi.ModelsInput;
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
            var aeroporto = _aeroportoService.Get(id);
            if (aeroporto == null)
                return NotFound();
            return aeroporto;
        }

        // POST api/<AeroportosController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Aeroporto>> Post([FromBody] AeroportoInput aeroportoInput)
        {
            var endereco = await ViaCepService.GetEndereco(aeroportoInput.Endereco.Cep);

            if (endereco == null)
                return BadRequest("Cep Inválido!");

            endereco.Numero = aeroportoInput.Endereco.Numero;
            endereco.Complemento = aeroportoInput.Endereco.Complemento;

            return _aeroportoService.Create(new Aeroporto { Nome = aeroportoInput.Nome, Sigla = aeroportoInput.Sigla, Endereco = endereco });
        }


        // PUT api/<AeroportosController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Put(string id, [FromBody] AeroportoInput aeroportoInput)
        {
            var aeroportoGet = _aeroportoService.Get(id);

            if (aeroportoGet == null)
                return NotFound(id);

            var endereco = await ViaCepService.GetEndereco(aeroportoInput.Endereco.Cep);

            if (endereco == null)
                return BadRequest("Cep Inválido!");

            endereco.Numero = aeroportoInput.Endereco.Numero;
            endereco.Complemento = aeroportoInput.Endereco.Complemento;

            _aeroportoService.Update(id, new Aeroporto { Nome = aeroportoInput.Nome, Sigla = aeroportoInput.Sigla, Endereco = endereco });
            return Ok();
        }

        // DELETE api/<AeroportosController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            var aeroporto = _aeroportoService.Get(id);
            if (aeroporto == null)
                return NotFound();
            _aeroportoService.Remove(id);
            return Ok();
        }
    }
}
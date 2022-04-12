using System.Collections.Generic;
using System.Threading.Tasks;
using AirlinesPassageiroApi.Models;
using AirlinesPassageiroApi.ModelsInput;
using AirlinesPassageiroApi.Services;
using AirlinesPassageiroApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesPassageiroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageirosController : ControllerBase
    {
        private readonly PassageiroService _passageiroService;
        public PassageirosController(PassageiroService passageiroService)
        {
            _passageiroService = passageiroService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult<IEnumerable<Passageiro>> Get() => _passageiroService.Get();

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult<Passageiro> Get(string id)
        {
            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
                return NotFound();

            return passageiro;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Passageiro>> Post(PassageiroInput passageiroInput)
        {
            if (!CpfValidator.CpfValido(passageiroInput.Cpf))
                return BadRequest("Cpf inválido!");

            var endereco = await ViaCepService.GetEndereco(passageiroInput.Endereco.Cep);

            if (endereco == null)
                return BadRequest("Cep Inválido!");

            endereco.Numero = passageiroInput.Endereco.Numero;
            endereco.Complemento = passageiroInput.Endereco.Complemento;

            return _passageiroService.Create(new Passageiro
            {
                Cpf = passageiroInput.Cpf,
                Nome = passageiroInput.Nome,
                Telefone = passageiroInput.Telefone,
                DataNasc = passageiroInput.DataNasc,
                Email = passageiroInput.Email,
                Endereco = endereco
            });
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Put(string id, Passageiro passageiroInput)
        {
            if (!CpfValidator.CpfValido(passageiroInput.Cpf))
                return BadRequest("Cpf inválido!");

            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
            {
                return NotFound();
            }
            _passageiroService.Update(id, passageiroInput);

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
            {
                return NotFound();
            }

            _passageiroService.Remove(id);

            return Ok();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlinesPassageiroApi.Models;
using AirlinesPassageiroApi.ModelsInput;
using AirlinesPassageiroApi.Producers;
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
            LogProducer.AddLog("Buscando Passageiro.");
            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
            {
                LogProducer.AddLog("Passageiro não encontrado.");
                return NotFound();
            }

            LogProducer.AddLog("Passageiro encontrado com sucesso!");
            return passageiro;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Passageiro>> Post(PassageiroInput passageiroInput)
        {
            LogProducer.AddLog("Criando novo Passageiro.");

            if (!CpfValidator.CpfValido(passageiroInput.Cpf))
            {
                LogProducer.AddLog("Cpf inválido!");
                return BadRequest("Cpf inválido!");
            }

            var endereco = await ViaCepService.GetEndereco(passageiroInput.Endereco.Cep);

            if (endereco == null)
            {
                LogProducer.AddLog("Cep não localizado!");
                return BadRequest("Cep Inválido!");
            }

            endereco.Numero = passageiroInput.Endereco.Numero;
            endereco.Complemento = passageiroInput.Endereco.Complemento;

            var passageiro =  _passageiroService.Create(new Passageiro
            {
                Cpf = passageiroInput.Cpf,
                Nome = passageiroInput.Nome,
                Telefone = passageiroInput.Telefone,
                DataNasc = passageiroInput.DataNasc,
                Email = passageiroInput.Email,
                Endereco = endereco
            });

            LogProducer.AddLog("Passageiro criado com sucesso!");
            return passageiro;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Put(string id, Passageiro passageiroInput)
        {
            LogProducer.AddLog("Alterando Passageiro.");

            if (!CpfValidator.CpfValido(passageiroInput.Cpf))
            {
                LogProducer.AddLog("Cpf inválido!");
                return BadRequest("Cpf inválido!");
            }

            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
            {
                LogProducer.AddLog("Passageiro não localizado.");
                return NotFound();
            }

            _passageiroService.Update(id, passageiroInput);
            LogProducer.AddLog("Passageiro alterado com sucesso!");
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo Passageiro.");

            var passageiro = _passageiroService.Get(id);
            if (passageiro == null)
            {
                LogProducer.AddLog("Passageiro não localizado!");
                return NotFound();
            }

            _passageiroService.Remove(id);

            LogProducer.AddLog("Passageiro removido com sucesso!");
            return Ok();
        }
    }
}
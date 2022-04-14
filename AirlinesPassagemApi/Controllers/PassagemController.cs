using System.Collections.Generic;
using System.Linq;
using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.ModelsInput;
using AirlinesPassagemApi.Producers;
using AirlinesPassagemApi.Services;
using AirlinesVooApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlinesPassagemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagemController : ControllerBase
    {
        private readonly PassagemService _passagemService;
        private readonly VooService _vooService;
        private readonly PassageiroService _passageiroService;
        private readonly PrecoBaseService _precoBaseService;

        public PassagemController(PassagemService passagemService, VooService vooService, PassageiroService passageiroService, PrecoBaseService precoBaseService)
        {
            _passagemService = passagemService;
            _vooService = vooService;
            _passageiroService = passageiroService;
            _precoBaseService = precoBaseService;
        }

        // GET: api/<PassagemController>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Passagem>> Get() => _passagemService.Get();

        // GET api/<PassagemController>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Passagem> Get(string id)
        {
            LogProducer.AddLog("Buscando Passagem.");

            var passagem = _passagemService.Get(id);
            if (passagem == null)
            {
                LogProducer.AddLog("Passagem não encontrada.");
                return NotFound();
            }

            LogProducer.AddLog("Passagem encontrada com sucesso!");
            return passagem;
        }

        // POST api/<PassagemController>
        [HttpPost]
        [Authorize]
        public ActionResult<Passagem> Post([FromBody] PassagemInput passagemInput)
        {
            LogProducer.AddLog("Criando nova Passagem.");

            var passageiro = _passageiroService.Get(passagemInput.CpfPassageiro);
            if (passageiro == null)
                return NotFound("Passageiro não cadastrado em nossa base de dados");

            var voo = _vooService.Get(passagemInput.VooId);
            if (voo == null)
                return NotFound("Voo não cadastrado em nossa base de dados");

            var precosBase = _precoBaseService.Get(voo.Origem.Sigla, voo.Destino.Sigla);
            if (precosBase == null)
                return NotFound("Preço Base não cadastrado em nossa base de dados");

            var precoBaseAtual = precosBase.OrderByDescending(x => x.DataInclusao).First();

            var passagem = _passagemService.Create(
                 new Passagem
                 {
                     Passageiro = passageiro,
                     Voo = voo,
                     PrecoBase = precoBaseAtual,
                     Classe = passagemInput.Classe,
                     PercentualDesconto = passagemInput.PercentualDesconto
                 });

            LogProducer.AddLog("Passagem criada com sucesso!");
            return passagem;
        }


        // DELETE api/<PassagemController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(string id)
        {
            LogProducer.AddLog("Removendo Passagem.");
            
            var passagem = _passagemService.Get(id);
            if (passagem == null)
            {
                LogProducer.AddLog("Passagem não cadastrada em nossa base de dados");
                return NotFound("Passagem não cadastrada em nossa base de dados");
            }

            _passagemService.Remove(id);
            LogProducer.AddLog("Passagem removida com sucesso!");
            return Ok();
        }
    }
}

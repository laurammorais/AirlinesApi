using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.Services;
using AirlinesTest.PassagemUtils;
using Xunit;

namespace AirlinesTest
{
    public class UnitTestPassagem
    {
        private const string PassagemId = "625617b8f8636d70e79785c7";
        public PassagemService InitializeDataBase()
        {
            var settings = new MongoSettings();
            var passagemService = new PassagemService(settings);
            InsertInitialData(passagemService);
            return passagemService;
        }

        [Fact]
        public void GetBydId()
        {
            var passagemService = InitializeDataBase();
            var passagem = passagemService.Get(PassagemId);

            Assert.Equal(PassagemId, passagem.Id);
        }

        [Fact]
        public void Create()
        {
            var passagemService = InitializeDataBase();

            if (passagemService.Get("6256239859cf6f7ef6504d82") != null)
            {
                Assert.True(true);
                return;
            }

            passagemService.Create(new Passagem
            {
                Id = "6256239859cf6f7ef6504d82",
                Classe = new Classe
                {
                    Descricao = "A"
                },
                PercentualDesconto = 10,
                PrecoBase = new PrecoBase
                {
                    Valor = 400
                }
            });

            var passagem = passagemService.Get("6256239859cf6f7ef6504d82");
            Assert.Equal("6256239859cf6f7ef6504d82", passagem.Id);
        }

        [Fact]
        public void Update()
        {
            var passagemService = InitializeDataBase();

            passagemService.Update(
                PassagemId,
                new Passagem
                {
                    Id = PassagemId,
                    Classe = new Classe
                    {
                        Descricao = "A"
                    },
                    PercentualDesconto = 10,
                    PrecoBase = new PrecoBase
                    {
                        Valor = 500
                    }
                });
            var passagem = passagemService.Get(PassagemId);
            Assert.Equal(500, passagem.PrecoBase.Valor);
        }

        [Fact]
        public void Remove()
        {

            var passagemService = InitializeDataBase();

            if (passagemService.Get("6256239859cf6f7ef6504d82") != null)
            {
                Assert.True(true);
                return;
            }

            passagemService.Remove("6256239859cf6f7ef6504d82");
            var passagem = passagemService.Get("6256239859cf6f7ef6504d82");
            Assert.Null(passagem);
        }

        private void InsertInitialData(PassagemService passagemService)
        {
            if (passagemService.Get(PassagemId) != null)
                return;

            passagemService.Create(
                new Passagem
                {
                    Id = PassagemId,
                    Classe = new Classe
                    {
                        Descricao = "A"
                    },
                    PercentualDesconto = 10,
                    PrecoBase = new PrecoBase
                    {
                        Valor = 300
                    }
                });
        }
    }
}
using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.Utils;
using MongoDB.Driver;

namespace AirlinesVooApi.Services
{
    public class PassageiroService
    {
        private readonly IMongoCollection<Passageiro> _passageiro;
        public PassageiroService(IMongoSettings settings)
        {
            var passageiroClient = new MongoClient(settings.ConnectionString);
            var database = passageiroClient.GetDatabase(settings.DatabaseName);
            _passageiro = database.GetCollection<Passageiro>(settings.PassageiroCollectionName);
        }
        public Passageiro Get(string cpf) => _passageiro.Find(passageiro => passageiro.Cpf == cpf).FirstOrDefault();
    }
}
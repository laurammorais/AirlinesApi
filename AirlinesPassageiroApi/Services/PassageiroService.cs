using System.Collections.Generic;
using AirlinesPassageiroApi.Models;
using AirlinesPassageiroApi.Utils;
using MongoDB.Driver;

namespace AirlinesPassageiroApi.Services
{
    public class PassageiroService
    {
        private readonly IMongoCollection<Passageiro> _passageiros;
        public PassageiroService(IMongoSettings settings)
        {
            var passageiro = new MongoClient(settings.ConnectionString);
            var database = passageiro.GetDatabase(settings.DatabaseName);
            _passageiros = database.GetCollection<Passageiro>(settings.PassageiroCollectionName);
        }

        public List<Passageiro> Get() => _passageiros.Find(passageiro => true).ToList();

        public Passageiro Get(string id) => _passageiros.Find(passageiro => passageiro.Id == id).FirstOrDefault();

        public Passageiro Create(Passageiro passageiro)
        {
            _passageiros.InsertOne(passageiro);
            return passageiro;
        }

        public void Update(string id, Passageiro passageiro) => _passageiros.ReplaceOne(x => x.Id == id, passageiro);

        public void Remove(string id) => _passageiros.DeleteOne(passageiro => passageiro.Id == id);
    }
}
using System.Collections.Generic;
using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.Utils;
using MongoDB.Driver;

namespace AirlinesPassagemApi.Services
{
    public class PassagemService
    {
        private readonly IMongoCollection<Passagem> _passagem;
        public PassagemService(IMongoSettings settings)
        {
            var passagemClient = new MongoClient(settings.ConnectionString);
            var database = passagemClient.GetDatabase(settings.DatabaseName);
            _passagem = database.GetCollection<Passagem>(settings.PassagemCollectionName);
        }

        public List<Passagem> Get() => _passagem.Find(passagem => true).ToList();

        public Passagem Get(string id) => _passagem.Find(passagem => passagem.Id == id).FirstOrDefault();

        public Passagem Create(Passagem passagem)
        {
            _passagem.InsertOne(passagem);
            return passagem;
        }

        public void Update(string id, Passagem passagem) => _passagem.ReplaceOne(x => x.Id == id, passagem);

        public void Remove(string id) => _passagem.DeleteOne(passagem => passagem.Id == id);
    }
}

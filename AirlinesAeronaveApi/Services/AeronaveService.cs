using System.Collections.Generic;
using AirlinesAeronaveApi.Models;
using AirlinesAeronaveApi.Utils;
using MongoDB.Driver;

namespace AirlinesAeronaveApi.Services
{
    public class AeronaveService
    {
        private readonly IMongoCollection<Aeronave> _aeronaves;
        public AeronaveService(IMongoSettings settings)
        {
            var aeronaveClient = new MongoClient(settings.ConnectionString);
            var database = aeronaveClient.GetDatabase(settings.DatabaseName);
            _aeronaves = database.GetCollection<Aeronave>(settings.AeronaveCollectionName);
        }

        public List<Aeronave> Get() => _aeronaves.Find(aeronave => true).ToList();

        public Aeronave Get(string id) => _aeronaves.Find(aeronave => aeronave.Id == id).FirstOrDefault();

        public Aeronave Create(Aeronave aeronave)
        {
            _aeronaves.InsertOne(aeronave);
            return aeronave;
        }

        public void Update(string id, Aeronave aeronave) => _aeronaves.ReplaceOne(x => x.Id == id, aeronave);

        public void Remove(string id) => _aeronaves.DeleteOne(aeronave => aeronave.Id == id);
    }
}
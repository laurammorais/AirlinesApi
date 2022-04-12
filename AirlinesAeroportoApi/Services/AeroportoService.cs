using System.Collections.Generic;
using AirlinesAeroportoApi.Models;
using AirlinesAeroportoApi.Utils;
using MongoDB.Driver;

namespace AirlinesAeroportoApi.Services
{
    public class AeroportoService
    {
        private readonly IMongoCollection<Aeroporto> _aeroportos;
        public AeroportoService(IMongoSettings settings)
        {
            var aeroporto = new MongoClient(settings.ConnectionString);
            var database = aeroporto.GetDatabase(settings.DatabaseName);
            _aeroportos = database.GetCollection<Aeroporto>(settings.AeroportoCollectionName);
        }

        public List<Aeroporto> Get() => _aeroportos.Find(aeroporto => true).ToList();

        public Aeroporto Get(string id) => _aeroportos.Find(aeroporto => aeroporto.Id == id).FirstOrDefault();

        public Aeroporto Create(Aeroporto aeroporto)
        {
            _aeroportos.InsertOne(aeroporto);
            return aeroporto;
        }

        public void Update(string id, Aeroporto aeroporto) => _aeroportos.ReplaceOne(x => x.Id == id, aeroporto);

        public void Remove(string id) => _aeroportos.DeleteOne(aeroporto => aeroporto.Id == id);
    }
}
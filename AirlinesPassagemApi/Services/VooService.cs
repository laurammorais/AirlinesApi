using System.Collections.Generic;
using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.Utils;
using MongoDB.Driver;

namespace AirlinesVooApi.Services
{
    public class VooService
    {
        private readonly IMongoCollection<Voo> _voo;
        public VooService(IMongoSettings settings)
        {
            var vooClient = new MongoClient(settings.ConnectionString);
            var database = vooClient.GetDatabase(settings.DatabaseName);
            _voo = database.GetCollection<Voo>(settings.VooCollectionName);
        }

        public List<Voo> Get() => _voo.Find(voo => true).ToList();

        public Voo Get(string id) => _voo.Find(voo => voo.Id == id).FirstOrDefault();

        public Voo Create(Voo voo)
        {
            _voo.InsertOne(voo);
            return voo;
        }

        public void Update(string id, Voo voo) => _voo.ReplaceOne(x => x.Id == id, voo);

        public void Remove(string id) => _voo.DeleteOne(voo => voo.Id == id);
    }
}

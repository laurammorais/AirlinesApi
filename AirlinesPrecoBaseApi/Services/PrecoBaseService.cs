using System.Collections.Generic;
using AirlinesPrecoBaseApi.Models;
using AirlinesPrecoBaseApi.Utils;
using MongoDB.Driver;

namespace AirlinesPrecoBaseApi.Sevices
{
    public class PrecoBaseService
    {
        private readonly IMongoCollection<PrecoBase> _precoBase;
        public PrecoBaseService(IMongoSettings settings)
        {
            var precoBaseClient = new MongoClient(settings.ConnectionString);
            var database = precoBaseClient.GetDatabase(settings.DatabaseName);
            _precoBase = database.GetCollection<PrecoBase>(settings.PrecoBaseCollectionName);
        }

        public List<PrecoBase> Get() => _precoBase.Find(precoBase => true).ToList();

        public PrecoBase Get(string id) => _precoBase.Find(precoBase => precoBase.Id == id).FirstOrDefault();

        public PrecoBase Create(PrecoBase precoBase)
        {
            _precoBase.InsertOne(precoBase);
            return precoBase;
        }

        public void Update(string id, PrecoBase precoBase) => _precoBase.ReplaceOne(x => x.Id == id, precoBase);

        public void Remove(string id) => _precoBase.DeleteOne(precoBase => precoBase.Id == id);
    }
}

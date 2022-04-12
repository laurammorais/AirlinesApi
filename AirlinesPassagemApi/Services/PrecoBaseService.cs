using System.Collections.Generic;
using AirlinesPassagemApi.Models;
using AirlinesPassagemApi.Utils;
using MongoDB.Driver;

namespace AirlinesPassagemApi.Services
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
        public List<PrecoBase> Get(string origemSigla, string destinoSigla) 
            => _precoBase.Find(precoBase => precoBase.Origem.Sigla == origemSigla && precoBase.Destino.Sigla == destinoSigla).ToList();
    }
}
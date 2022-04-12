using AirlinesVooApi.Models;
using AirlinesVooApi.Utils;
using MongoDB.Driver;

namespace AirlinesVooApi.Services
{
    public class AeroportoService
    {
        private readonly IMongoCollection<Aeroporto> _aeroporto;
        public AeroportoService(IMongoSettings settings)
        {
            var aeroportoClient = new MongoClient(settings.ConnectionString);
            var database = aeroportoClient.GetDatabase(settings.DatabaseName);
            _aeroporto = database.GetCollection<Aeroporto>(settings.AeroportoCollectionName);
        }
        public Aeroporto Get(string sigla) => _aeroporto.Find(aeroporto => aeroporto.Sigla == sigla).FirstOrDefault();
    }
}
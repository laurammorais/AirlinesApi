using AirlinesPrecoBaseApi.Models;
using AirlinesPrecoBaseApi.Utils;
using MongoDB.Driver;

namespace AirlinesPrecoBaseApi.Sevices
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
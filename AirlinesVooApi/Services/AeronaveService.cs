using AirlinesVooApi.Models;
using AirlinesVooApi.Utils;
using MongoDB.Driver;

namespace AirlinesVooApi.Services
{
    public class AeronaveService
    {
        private readonly IMongoCollection<Aeronave> _aeronave;
        public AeronaveService(IMongoSettings settings)
        {
            var aeronaveClient = new MongoClient(settings.ConnectionString);
            var database = aeronaveClient.GetDatabase(settings.DatabaseName);
            _aeronave = database.GetCollection<Aeronave>(settings.AeronaveCollectionName);
        }
        public Aeronave Get(string id) => _aeronave.Find(aeronave => aeronave.Id == id).FirstOrDefault();
    }
}
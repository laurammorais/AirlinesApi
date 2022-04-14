using System.Collections.Generic;
using AirlinesLogApi.Models;
using AirlinesLogApi.Utils;
using MongoDB.Driver;

namespace AirlinesLogApi.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> _log;
        public LogService(IMongoSettings settings)
        {
            var logClient = new MongoClient(settings.ConnectionString);
            var database = logClient.GetDatabase(settings.DatabaseName);
            _log = database.GetCollection<Log>(settings.LogCollectionName);
        }

        public List<Log> Get() => _log.Find(log => true).ToList();

        public Log Get(string id) => _log.Find(log => log.Id == id).FirstOrDefault();

        public Log Create(Log log)
        {
            _log.InsertOne(log);
            return log;
        }
    }
}

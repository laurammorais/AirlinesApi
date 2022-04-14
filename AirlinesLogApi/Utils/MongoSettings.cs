namespace AirlinesLogApi.Utils
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string LogCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}

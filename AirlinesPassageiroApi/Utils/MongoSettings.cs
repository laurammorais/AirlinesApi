namespace AirlinesPassageiroApi.Utils
{
    public class MongoSettings : IMongoSettings
    {
        public string PassageiroCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
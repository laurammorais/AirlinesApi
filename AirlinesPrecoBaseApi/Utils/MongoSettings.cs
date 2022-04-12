namespace AirlinesPrecoBaseApi.Utils
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string PrecoBaseCollectionName { get; set; }
        public string AeroportoCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
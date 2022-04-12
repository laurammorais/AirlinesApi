namespace AirlinesPassagemApi.Utils
{
    public class MongoSettings : IMongoSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string PassagemCollectionName { get; set; }
        public string VooCollectionName { get; set; }
        public string PassageiroCollectionName { get; set; }
        public string PrecoBaseCollectionName { get; set; }

    }
}

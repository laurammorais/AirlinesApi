using AirlinesPassagemApi.Utils;

namespace AirlinesTest.PassagemUtils
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "Tests";
        public string PassagemCollectionName { get; set; } = "Passagem";
        public string VooCollectionName { get; set; } = "Voo";
        public string PassageiroCollectionName { get; set; } = "Passageiro";
        public string PrecoBaseCollectionName { get; set; } = "PrecoBase";
    }
}
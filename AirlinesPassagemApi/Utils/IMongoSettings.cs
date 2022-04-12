using MongoDB.Driver;

namespace AirlinesPassagemApi.Utils
{
    public interface IMongoSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string PassagemCollectionName { get; set; }
        string VooCollectionName { get; set; }
        string PassageiroCollectionName { get; set; }
        string PrecoBaseCollectionName { get; set; }
    }
}

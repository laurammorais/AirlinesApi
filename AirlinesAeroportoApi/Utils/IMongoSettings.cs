using MongoDB.Driver;

namespace AirlinesAeroportoApi.Utils
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string AeroportoCollectionName { get; }
        string DatabaseName { get; set; }
    }
}
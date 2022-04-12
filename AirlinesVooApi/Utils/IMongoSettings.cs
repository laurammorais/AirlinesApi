namespace AirlinesVooApi.Utils
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string VooCollectionName { get; set; }
        string AeroportoCollectionName { get; set; }
        string AeronaveCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}

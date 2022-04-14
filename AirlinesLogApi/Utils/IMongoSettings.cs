namespace AirlinesLogApi.Utils
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string LogCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}

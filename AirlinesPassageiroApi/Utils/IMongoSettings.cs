namespace AirlinesPassageiroApi.Utils
{
    public interface IMongoSettings
    {
        string PassageiroCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
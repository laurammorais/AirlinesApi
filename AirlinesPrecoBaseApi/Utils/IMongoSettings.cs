namespace AirlinesPrecoBaseApi.Utils
{
    public interface IMongoSettings
    {
        string ConnectionString{ get; set; }
        string PrecoBaseCollectionName { get; set; }
        string AeroportoCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
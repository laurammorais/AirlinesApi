using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPrecoBaseApi.Models
{
    public class Aeroporto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string LoguinUser { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesAeronaveApi.Models
{
    public class Aeronave
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public string Nome { get; set; }
        public int Capacidade { get; set; }
    }
}
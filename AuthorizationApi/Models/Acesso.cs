using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthorizationApi.Models
{
    public class Acesso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public string Descricao { get; set;}
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPassagemApi.Models
{
    public class Aeroporto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}

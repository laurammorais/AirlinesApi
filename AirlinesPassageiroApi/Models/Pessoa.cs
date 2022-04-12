using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPassageiroApi.Models
{
    public class Pessoa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
        public string LoginUser { get; set; }
    }
}
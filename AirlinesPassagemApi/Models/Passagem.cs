using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPassagemApi.Models
{
    public class Passagem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public Voo Voo { get; set; }
        public Passageiro Passageiro { get; set; }
        public PrecoBase PrecoBase { get; set; }
        public Classe Classe { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public decimal ValorTotal { get; set; }
        public decimal PercentualDesconto { get; set; }
        public string LoginUser { get; set; }
    }
}
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPassagemApi.Models
{
    public class Passagem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public decimal Preco => PrecoBase.Valor - PrecoBase.Valor * (PercentualDesconto / 100);
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public Classe Classe { get; set; }
        public PrecoBase PrecoBase { get; set; }
        public decimal PercentualDesconto { get; set; }
        public Passageiro Passageiro { get; set; }
        public Voo Voo { get; set; }
    }
}

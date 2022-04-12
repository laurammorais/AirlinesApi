using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPassagemApi.Models
{
    public class PrecoBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Aeroporto Origem { get; set; }
        public Aeroporto Destino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataInclusao { get; private set; } = DateTime.Now;
        public string LoguinUser { get; set; }
    }
}
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesPrecoBaseApi.Models
{
    public class PrecoBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public Aeroporto Origem { get; set; }
        public Aeroporto Destino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataInclusao { get; private set; } = DateTime.Now;
    }
}

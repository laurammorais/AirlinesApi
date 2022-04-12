using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesVooApi.Models
{
    public class Voo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public Aeroporto Destino { get; set; }
        public Aeroporto Origem { get; set; }
        public Aeronave Aeronave { get; set; }
        public DateTime HorarioEmbarque { get; set; }
        public DateTime HorarioDesembarque { get; set; }
        public string LoginUser { get; set; }
    }
}
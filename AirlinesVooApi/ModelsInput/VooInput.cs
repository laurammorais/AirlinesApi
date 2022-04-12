using System;

namespace AirlinesVooApi.ModelsInput
{
    public class VooInput
    {
        public string OrigemSigla { get; set; }
        public string DestinoSigla { get; set; }
        public DateTime HorarioEmbarque { get; set; }
        public DateTime HorarioDesembarque { get; set; }
        public string AeronaveId { get; set; }
    }
}

using AirlinesPassagemApi.Models;

namespace AirlinesPassagemApi.ModelsInput
{
    public class PassagemInput
    {
        public decimal PercentualDesconto { get; set; }
        public string CpfPassageiro { get; set; }
        public Classe Classe { get; set; }
        public string VooId { get; set; }
    }
}

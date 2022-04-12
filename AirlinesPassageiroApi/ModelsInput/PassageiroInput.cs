using System;

namespace AirlinesPassageiroApi.ModelsInput
{
    public class PassageiroInput
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public string Email { get; set; }
        public EnderecoInput Endereco { get; set; }
    }
}

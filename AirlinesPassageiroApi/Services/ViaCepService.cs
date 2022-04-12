using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AirlinesPassageiroApi.Models;
using AirlinesPassageiroApi.ModelsInput;
using Newtonsoft.Json;

namespace AirlinesPassageiroApi.Services
{
    public class ViaCepService
    {
        public static async Task<Endereco> GetEndereco(string cep)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"https://viacep.com.br/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
                return null;

            var enderecoString = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<EnderecoViaCep>(enderecoString);

            return new Endereco { Cep = endereco.Cep, Logradouro = endereco.Logradouro, Bairro = endereco.Bairro, Cidade = endereco.Localidade, Estado = endereco.Uf };
        }
    }
}

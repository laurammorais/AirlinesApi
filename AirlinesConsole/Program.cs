using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AirlinesConsole.Models;
using Newtonsoft.Json;

namespace AirlinesConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Console.Write("Usuario: ");
                var user = Console.ReadLine();

                Console.Write("Senha: ");
                var password = Console.ReadLine();

                var tokenResponse = await client.PostAsync($"https://localhost:44380/api/Authorization/login?username={user}&password={password}", null);

                var tokenString = await tokenResponse.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<dynamic>(tokenString).token.Value;


                var escolha = 1;

                while (escolha == 1)
                {
                    Console.Write("Digite a Sigla do Aeroporto: ");
                    var sigla = Console.ReadLine();

                    Console.Write("Digite o Nome do Aeroporto: ");
                    var nome = Console.ReadLine();

                    Console.Write("Digite o Cep do Aeroporto: ");
                    var cep = Console.ReadLine();

                    Console.Write("Digite o Número do Endereço do Aeroporto: ");
                    var numero = int.Parse(Console.ReadLine());

                    Console.Write("Digite o Complemento do Aeroporto: ");
                    var complemento = Console.ReadLine();

                    var aeroporto = new Aeroporto
                    {
                        Sigla = sigla,
                        Nome = nome,
                        Endereco = new Endereco
                        {
                            Cep = cep,
                            Numero = numero,
                            Complemento = complemento,
                        }
                    };

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await client.PostAsJsonAsync("http://localhost:59537/api/aeroportos", aeroporto);
                    response.EnsureSuccessStatusCode();

                    Console.Write("Digite 1 para adicionar outro aeroporto: ");
                    escolha = int.Parse(Console.ReadLine());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Api Indisponível!");
            }
        }
    }
}
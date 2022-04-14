using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AirlinesLogConsumer.Models;

namespace AirlinesLogConsumer.Services
{
    public class AirlinesLogClient
    {
        public static async Task Add(Log message)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:44370/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                await client.PostAsJsonAsync("api/Log", message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Message : {ex.Message}");
            }
        }
    }
}
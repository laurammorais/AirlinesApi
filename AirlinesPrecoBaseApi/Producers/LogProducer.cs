using System.Text;
using AirlinesPrecoBaseApi.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AirlinesPrecoBaseApi.Producers
{
    public class LogProducer
    {
        private static readonly ConnectionFactory _factory = new() { HostName = "localhost" };
        private const string QUEUE_NAME = "messagelogs";
        public static void AddLog(string description)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: QUEUE_NAME,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var stringfieldMessage = JsonConvert.SerializeObject(new Log(description));
            var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

            channel.BasicPublish(
                exchange: "",
                routingKey: QUEUE_NAME,
                basicProperties: null,
                body: bytesMessage
                );
        }
    }
}
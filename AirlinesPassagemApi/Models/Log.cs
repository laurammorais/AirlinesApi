namespace AirlinesPassagemApi.Models
{
    public class Log
    {
        public Log(string description) => Description = description;

        public string Description { get; private set; }
    }
}
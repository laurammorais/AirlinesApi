using System.Collections.Generic;
using System.Linq;
using Authorization.Models;

namespace Authorization.Repositories
{
    public class Repositories
    {
        public static Token Get(string username, string password)
        {
            var users = new List<Token>
            {
                new Token { Id = 1, Username = "admin", Password = "admin", Role = "manager" },
                new Token { Id = 2, Username = "user", Password = "user", Role = "employee" }
            };

            return users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }
    }
}
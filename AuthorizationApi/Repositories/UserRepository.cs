using System.Collections.Generic;
using System.Linq;
using AuthorizationApi.Models;

namespace AuthorizationApi.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "admin", Password = "admin", Role = "manager" },
                new User { Id = 2, Username = "user", Password = "user", Role = "employee" }
            };

            return users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }
    }
}
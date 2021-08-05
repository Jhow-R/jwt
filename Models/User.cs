using System.Text.Json.Serialization;

namespace TesteJWT.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal string Role { get; set; }
    }
}

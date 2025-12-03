using Auth1Project.Models;

namespace Auth1Project.Services
{
    public class AuthService
    {
        private List<User> _users = new List<User>
        {
            new User {
                Id = 1,
                Username = "admin",
                Password = "admin123",
                Role = "Admin",
                Email = "admin@example.com"
            },
            new User {
                Id = 2,
                Username = "user",
                Password = "user123",
                Role = "User",
                Email = "user@example.com"
            },
            new User {
                Id = 3,
                Username = "guest",
                Password = "guest123",
                Role = "Guest" 
            }
        };
    }
}

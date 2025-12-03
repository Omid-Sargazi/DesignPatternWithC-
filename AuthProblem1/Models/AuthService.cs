namespace AuthProblem1.Models
{
    public class AuthService
    {
        // لیست کاربران تستی
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", PasswordHash = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "user", PasswordHash = "user123", Role = "User" }
        };

        public User? Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username && u.PasswordHash == password);
        }
    }

}

using UserManagement.Domain.Enums;

namespace UserManagement.ViewModels.User
{
    public class RegisterUserView
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}

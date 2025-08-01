using UserManagement.Enums;

namespace UserManagement.ViewModels.User
{
    public class UserView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public UserStatus Status { get; set; }
    }
}

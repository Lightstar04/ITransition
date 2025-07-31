using UserManagement.Domain.Entities;
using UserManagement.ViewModels.User;

namespace UserManagement.Extensions
{
    public static class UserMappings
    {
        public static UserView ToView(this User user)
        {
            return new UserView
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                LastLoginTime = user.LastLoginTime,
                Status = user.Status,
            };
        }

        public static User ToEntity(this RegisterUserView view)
        {
            return new User
            {
                FullName = view.FullName,
                Email = view.Email,
                Password = view.Password
            };
        }
    }
}

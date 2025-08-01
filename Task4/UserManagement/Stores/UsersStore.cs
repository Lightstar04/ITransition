using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Exceptions;
using UserManagement.Infrastructure;
using UserManagement.ViewModels.User;

namespace UserManagement.Stores
{
    public class UsersStore
    {
        public List<User> Get(string? search)
        {
            using var context = new UserManagementDbContext();
            var query = context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FullName.Contains(search));
            }

            return query.AsNoTracking().ToList();
        }

        public void SignUp(User user)
        {
            using var context = new UserManagementDbContext();

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch(DbUpdateException ex) when(ex.InnerException?.Message.Contains("IX_Users_Email") == true)
            {
                throw new EntityExistsException("User already exists");
            }
        }

        public bool SignIn(string email, string password)
        {
            using var context = new UserManagementDbContext();
            
            var user = context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.Status == UserStatus.Blocked || user.Password != password)
            {
                return false;
            }

            user.LastLoginTime = DateTime.Now;
            context.SaveChanges();

            return true;
        }

        public void ButtonOperation(string[] selected, string operation)
        {
            using var context = new UserManagementDbContext();

            var ids  = selected.Select(int.Parse).ToList();
            var users = context.Users.Where(u => ids.Contains(u.Id)).ToList();

            if (operation == "block")
                users.ForEach(u => u.Status = UserStatus.Blocked);
            else if (operation == "unblock")
                users.ForEach(u => u.Status = UserStatus.Active);
            else if (operation == "delete")
                context.Users.RemoveRange(users);

            context.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            using var context = new UserManagementDbContext();
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}

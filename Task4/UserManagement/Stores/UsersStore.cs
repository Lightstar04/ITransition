using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Entities;
using UserManagement.Enums;
using UserManagement.Exceptions;

namespace UserManagement.Stores
{
    public class UsersStore
    {
        private readonly UserManagementDbContext _context;

        public UsersStore(UserManagementDbContext context)
        {
            _context = context;
        }
        public List<User> Get(string? search)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FullName.Contains(search));
            }

            return query.AsNoTracking().ToList();
        }

        public void SignUp(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch(DbUpdateException ex) when(ex.InnerException?.Message.Contains("IX_Users_Email") == true)
            {
                throw new EntityExistsException("User already exists");
            }
        }

        public bool SignIn(string email, string password)
        {            
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.Status == UserStatus.Blocked || user.Password != password)
            {
                return false;
            }

            user.LastLoginTime = DateTime.Now;
            _context.SaveChanges();

            return true;
        }

        public void ButtonOperation(string[] selected, string operation)
        {
            var ids  = selected.Select(int.Parse).ToList();
            var users = _context.Users.Where(u => ids.Contains(u.Id)).ToList();

            if (operation == "block")
                users.ForEach(u => u.Status = UserStatus.Blocked);
            else if (operation == "unblock")
                users.ForEach(u => u.Status = UserStatus.Active);
            else if (operation == "delete")
                _context.Users.RemoveRange(users);

            _context.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}

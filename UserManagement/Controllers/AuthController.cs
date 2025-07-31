using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Entities;
using UserManagement.Extensions;
using UserManagement.Stores;
using UserManagement.ViewModels.User;

namespace UserManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsersStore _store;
        private readonly IPasswordHasher<User> _hasher;

        public AuthController()
        {
            _store = new UsersStore();
            _hasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterUserView user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                var entity = user.ToEntity();
                _store.SignUp(entity);
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var result = _store.SignIn(email, password);
            if(!result)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Users");
        }
    }
}

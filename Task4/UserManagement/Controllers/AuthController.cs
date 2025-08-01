using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Entities;
using UserManagement.Extensions;
using UserManagement.Stores;
using UserManagement.ViewModels.User;

namespace UserManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsersStore _store;
        private readonly IPasswordHasher<User> _hasher;

        public AuthController(UsersStore store)
        {
            _store = store;
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
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = "User already exists";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var result = _store.SignIn(email, password);
            
            if (!result)
            {
                ViewData["ErrorMessage"] = "Password is wrong or you might be blocked. Please try again!";
                return View();
            }

            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Users");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Enums;
using UserManagement.Extensions;
using UserManagement.Stores;

namespace UserManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UsersStore _store;

        public UsersController(UsersStore store)
        {
            _store = store;
        }

        public IActionResult Index(string? search)
        {
            var usersList = _store.Get(search);
            var userViews = usersList.Select(x => x.ToView());

            ViewBag.Users = usersList;
            
            return View(userViews);
        }

        public IActionResult Action(string[] selected, string operation)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth");
            }

            var currentUser = _store.GetUserByEmail(email);
            if (currentUser == null || currentUser.Status == UserStatus.Blocked)
            {
                return RedirectToAction("Login", "Auth");
            }

            _store.ButtonOperation(selected, operation);

            return RedirectToAction("Index");
        }
    }
}

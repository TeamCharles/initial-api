using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonWeb.ViewModels;

namespace BangazonWeb.Controllers
{
    /**
     * Class: UserController
     * Purpose: Provide methods for adding a new user to the database
     * Author: Matt Kraatz
     * Methods:
     *   BangazonContext context - store a reference to the database context
     *   UserController(BangazonContext ctx) - constructor run at setup
     *   IActionResult New() - returns new user creation form view
     *   IActionResult New(User user) - posts a new user to the database via form
     *   IActionResult Error() - returns Error page
     *   bool UserExists(int id) - checks the database for potential duplicate users
     */
    public class UserController : Controller
    {
        private BangazonContext context;

        public UserController(BangazonContext ctx)
        {
            context = ctx;
        }

        public IActionResult New()
        {
          var model = new UserCreate(context);
          return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(UserCreate user)
        {
            if (!ModelState.IsValid)
            {
                var model = new UserCreate(context);
                model.NewUser = user.NewUser;
                return View(model);
            }

            context.User.Add(user.NewUser);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.NewUser.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            ActivateUser(user.NewUser);
            return RedirectToAction("Buy", "ProductTypes");
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            ActivateUser(context.User.Single(u => u.UserId == id));
            return Json(new {result = "User updated successfully"});
        }

        public IActionResult Error()
        {
            return View();
        }

        private bool UserExists(int id)
        {
            return context.User.Count(e => e.UserId == id) > 0;
        }

        private void ActivateUser(User user)
        {
            ActiveUser singleton = ActiveUser.Instance;
            singleton.User = user;
        }
    }
}

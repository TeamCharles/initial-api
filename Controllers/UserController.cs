using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Helpers;
using BangazonWeb.ViewModels;

namespace BangazonWeb.Controllers
{
    /**
     * Class: UserController
     * Purpose: Provide methods for adding a new user to the database
     * Author: Matt Kraatz
     * Methods:
     *   IActionResult New() - Returns new user creation form view.
     *   IActionResult New(UserCreate user) - Posts a new user to the database via form. Sets that user to the Active User. Redirects user to the ProductTypes/Buy page.
     *          - UserCreate user: UserCreate viewmodel provided on submission of the form.
     *   IActionResult Activate(int id) - Logs in a user via Post.
     *          - int id: UserId of the user being logged in.
     *   IActionResult Error() - returns Error page. Currently not in use.
     *   bool UserExists(int id) - returns false if there is a duplicate user in the database.
     *          - int id: UserId for the user being queried.
     *   void ActivateUser(User user) - sets the ActiveUser globally to the provided user object.
     *          - User user: User object being set as the ActiveUser.
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

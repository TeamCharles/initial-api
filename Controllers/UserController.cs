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
     *   void UserController(BangazonContext ctx) - Constructor that saves a reference to the database context.
     *   IActionResult New() - Returns new user creation form view.
     *   IActionResult New(UserCreate user) - Posts a new user to the database via form. Sets that user to the Active User. Redirects user to the ProductTypes/Buy page.
     *   IActionResult Activate(int id) - Logs in a user via Post.
     *   bool UserExists(int id) - Checks for a duplicate user in the database.
     *   void ActivateUser(User user) - sets the ActiveUser globally to the provided user object.
     */
    public class UserController : Controller
    {
        private BangazonContext context;

        /**
         * Purpose: Constructor that saves a reference to the database context in a private variable.
         * Arguments:
         *      ctx - Database context.
         * Return:
         *      An instance of the class.
         */
        public UserController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Returns new user creation form view.
         * Arguments:
         *      Void
         * Return:
         *      New user creation form view.
         */
        public IActionResult New()
        {
          var model = new UserCreate(context);
          return View(model);
        }

        /**
         * Purpose: Posts a new user to the database via form. Sets that user to the Active User. Redirects user to the ProductTypes/Buy page.
         * Arguments:
         *      user - UserCreate viewmodel provided on submission of the form.
         * Return:
         *      If model is valid, redirects user to the ProductTypes/Buy method.
         *      If model is invalid, returns the UserCreate view with validation messages.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(UserCreate user)
        {
            // Checks for form completion
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
            // Logs in the new user
            ActivateUser(user.NewUser);
            return RedirectToAction("Buy", "ProductTypes");
        }

        /**
         * Purpose: Logs in a user via Post.
         * Arguments:
         *      id - UserId of the user being logged in.
         * Return:
         *      JSON response with success message. Front-end code handles a redirect.
         */
        [HttpPost]
        public IActionResult Activate(int id)
        {
            ActivateUser(context.User.Single(u => u.UserId == id));
            return Json(new {result = "User updated successfully"});
        }

        /**
         * Purpose: Checks for a duplicate user in the database.
         * Arguments:
         *      id - UserId for the user being queried.
         * Return:
         *      Returns true if there is a duplicate user, false if there is no duplicate user
         */
        private bool UserExists(int id)
        {
            return context.User.Count(e => e.UserId == id) > 0;
        }

        /**
         * Purpose: Sets the ActiveUser globally to the provided user object.
         * Arguments:
         *      user - User object being set as the ActiveUser.
         * Return:
         *      Void
         */
        private void ActivateUser(User user)
        {
            ActiveUser singleton = ActiveUser.Instance;
            singleton.User = user;
        }
    }
}

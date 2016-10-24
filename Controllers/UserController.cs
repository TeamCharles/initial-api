using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
          return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            context.User.Add(user);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Error()
        {
            return View();
        }

        private bool UserExists(int id)
        {
            return context.User.Count(e => e.UserId == id) > 0;
        }
    }
}

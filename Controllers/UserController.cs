using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
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
        public IActionResult New([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return Redirect("/");
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

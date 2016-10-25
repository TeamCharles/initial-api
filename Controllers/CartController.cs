using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Helpers;
using Bangazon.Models;

namespace BangazonWeb.Controllers
{
    public class CartController : Controller
    {
        private BangazonContext context;

        public CartController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            User ActiveUser = context.User.Single(u => u.UserId == 1);

            SessionHelper.ActiveUser = ActiveUser;

            var activeProducts =
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == context.Order.SingleOrDefault(o => o.DateCompleted == null && o.UserId == ActiveUser.UserId).OrderId && lineItem.ProductId == product.ProductId)
                select product;

            if (activeProducts == null)
            {
                // TODO: This should probably be returning something other than `View()`.
                return View();
            }

            return View(activeProducts);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

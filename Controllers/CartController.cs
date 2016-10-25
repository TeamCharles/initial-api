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
using Microsoft.AspNetCore.Routing;

namespace BangazonWeb.Controllers
{
    /**
     * Class: CartController
     * Purpose: Controls logged in user's cart
     * Author: Matt Hamil
     * Methods:
     *   Task<IActionResult> Index() - Queries for all products on user's active order and renders view
     *   IActionResult Error() - Renders an error
     */
    public class CartController : Controller
    {
        private BangazonContext context;

        public CartController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: This is a placeholder value. These two lines should be removed after the User Accounts dropdown works
            SessionHelper.ActiveUser = 1;

            ViewBag.Users = Users.GetAllUsers(context);

            // For help with this LINQ query, refer to
            // https://stackoverflow.com/questions/373541/how-to-do-joins-in-linq-on-multiple-fields-in-single-join
            var activeProducts =
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == context.Order.SingleOrDefault(o => o.DateCompleted == null && o.UserId == SessionHelper.ActiveUser).OrderId && lineItem.ProductId == product.ProductId)
                select product;

            if (activeProducts == null)
            {
                // Redirect to ProductTypes
                return RedirectToAction("Index", "ProductTypes");
            }

            float totalPrice = 0;
            foreach (var product in activeProducts.ToList())
            {
                totalPrice += product.Price;
            }

            ViewBag.totalPrice = totalPrice;

            return View(activeProducts);
        }

        public IActionResult Error()
        {
            ViewBag.Users = Users.GetAllUsers(context);
            return View();
        }
    }
}

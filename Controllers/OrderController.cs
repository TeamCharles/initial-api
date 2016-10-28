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
using BangazonWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.Controllers
{
    /**
     * Class: CartController
     * Purpose: Controls logged in user's cart
     * Author: Matt Hamil
     * Methods:
     *   Task<IActionResult> Index() - Queries for all products on user's active order and renders view
     *   Task<IActionResult> CreateNewOrder() - Creates a new open order for the customer
     *   Task<IActionResult> AddToCart() - Adds a product to a user's open order
     *   Task<IActionResult> DeleteLineItem() - Deletes a LineItem from the cart
     *   IActionResult Error() - Renders an error
     *   CompleteOrder() - Adds a completed date to the new order
     */
    public class OrderController : Controller
    {   
        private BangazonContext context;

        public OrderController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Final([FromRoute] int id)
        {
            // TODO: This is a placeholder value. These two lines should be removed after the User Accounts dropdown works
            
            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;
            if (userId == null)
            {
                return Redirect("ProductTypes");
            }

            // For help with this LINQ query, refer to
            // https://stackoverflow.com/questions/373541/how-to-do-joins-in-linq-on-multiple-fields-in-single-join
            var activeProducts = await(
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == context.Order.SingleOrDefault(o => o.DateCompleted == null && o.UserId == userId).OrderId && lineItem.ProductId == product.ProductId)
                select product).ToListAsync();

            if (activeProducts == null)
            {
                // Redirect to ProductTypes
                return RedirectToAction("Index", "ProductTypes");
            }

            var model = new OrderView(context);
            model.ActiveProducts = activeProducts;


            if (id > 0)
            {
                model.selectedPaymentId = id;
            }

            foreach (var product in activeProducts)
            {
                model.TotalPrice += product.Price;
            }

            model.AvailablePaymentType = 
                from PaymentType in context.PaymentType
                orderby PaymentType.Description
                where PaymentType.UserId == userId
                select new SelectListItem {
                    Text = PaymentType.Description,
                    Value = PaymentType.PaymentTypeId.ToString()
                    };

            return View(model);
        }
        public IActionResult Error()
        {
            
            ViewBag.Users = Users.GetAllUsers(context);
            return View();
        }
    }
}

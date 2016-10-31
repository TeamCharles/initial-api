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
     * Class: OrderController
     * Purpose: Controls logged in user's cart
     * Author: Anulfo Ordaz / Matt Kraatz / Dayne Writght / Garret Vangilder
     * Methods:
     *   OrderController(BangazonContext ctx) - Bring the context back
     *   Task<IActionResult> Final() - It retrieves the data for the drop
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

            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;
            if (userId == null)
            {
                return Redirect("ProductTypes");
            }

            var activeProducts = await(
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == context.Order.SingleOrDefault(o => o.DateCompleted == null && o.UserId == userId).OrderId && lineItem.ProductId == product.ProductId && lineItem.Product.IsActive == true)
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

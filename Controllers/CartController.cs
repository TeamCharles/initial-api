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

            var model = new CartView(context);
            model.ActiveProducts = activeProducts;

            foreach (var product in activeProducts)
            {
                model.TotalPrice += product.Price;
            }

            return View(model);
        }

        public async Task<IActionResult> AddToCart([FromRoute]int id)
        {
            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;

            // Find the product
            Product productQuery = await(
                from product in context.Product
                where product.ProductId == id
                select product).SingleOrDefaultAsync();

            if (productQuery == null)
            {
                return NotFound();
            }

            // Find the user's active order
            Order openOrderQuery = await(
                from order in context.Order
                where order.UserId == userId && order.DateCompleted == null
                select order).SingleOrDefaultAsync();

            Order openOrder = null;

            // If the user does not have an open order
            if (openOrderQuery == null)
            {
                // Creating a new Order
                openOrder = new Order {    
                    UserId = (int)ActiveUser.Instance.User.UserId
                };
                context.Order.Add(openOrder);
                await context.SaveChangesAsync();
            }
            else
            {
                openOrder = openOrderQuery;
            }

            // Create a new LineItem with the ProductId and OrderId
            LineItem lineItem = new LineItem(){ OrderId = openOrder.OrderId, ProductId = id };

            context.LineItem.Add(lineItem);
            await context.SaveChangesAsync();

            return RedirectToAction( "Detail", new RouteValueDictionary(
                     new { controller = "Products", action = "Detail", Id = id } ) );
        }


        public async Task<IActionResult> DeleteLineItem([FromRoute]int id)
        {
            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;
            
            Order OpenOrder = await(
                from order in context.Order

                where order.UserId == ActiveUser.Instance.User.UserId && order.DateCompleted == null
                select order).SingleOrDefaultAsync();  
            

            LineItem deletedItem = await context.LineItem.SingleAsync(p => p.ProductId == id && p.OrderId == OpenOrder.OrderId);


            if (deletedItem == null)
            {
                return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "Cart", action = "Index"} ) );   
            }

            try
            {
                context.Remove(deletedItem);
                await context.SaveChangesAsync();
                return RedirectToAction( "Index", new RouteValueDictionary(
                     new { controller = "Cart", action = "Index", Id = id } ) ); 
            }
            catch (DbUpdateException)
            {
                throw;
            }   
        }

        public async Task<IActionResult> CompleteOrder([FromRoute]int id)
        {
            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;
            if (userId == null)
            {
                return Redirect("ProductTypes");
            }
            Order openOrder = await(
                from order in context.Order
                where order.UserId == SessionHelper.ActiveUser && order.DateCompleted == null
                select order).SingleOrDefaultAsync();

            if (openOrder == null)
            {
                return RedirectToAction("ProductTypes", "Buy");
            }

            try
            {
                openOrder.DateCompleted = DateTime.Now;
                context.Order.Update(openOrder);
                await context.SaveChangesAsync();
                return RedirectToAction("Index","Cart");

            }
            catch (DbUpdateException)
            {
                throw;
            }   
        }

        public IActionResult Error()
        {
            
            ViewBag.Users = Users.GetAllUsers(context);
            return View();
        }
    }
}

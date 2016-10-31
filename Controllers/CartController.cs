using System;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using Microsoft.AspNetCore.Routing;
using BangazonWeb.ViewModels;

namespace BangazonWeb.Controllers
{
    /**
     * Class: CartController
     * Purpose: Controls logged in user's cart
     * Author: Matt Hamil/Dayne Wright
     * Methods:
     *   IActionResult Index() - Adds logged in user and checks view model.  Sends view model to view file.
     *   Task<IActionResult> CreateNewOrder() - Creates a new open order for the customer
     *   Task<IActionResult> AddToCart() - Adds a product to a user's open order
     *   Task<IActionResult> DeleteLineItem() - Deletes a LineItem from the cart
     *   IActionResult Error() - Renders an error
     *   CompleteOrder() - Adds a completed date to the new order
     *   Confirmation() - Changes the user's view to a completed order form.
     *   DeleteLineItem() - Deletes a lineitem from the order for the user.!--
     *   AddToCart() - Adds an active product to a user's cart.
     */
    public class CartController : Controller
    {
        private BangazonContext context;

        public CartController(BangazonContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var model = new CartView(context);

            if (model.CartProducts == null)
            {
                // Redirect to ProductTypes
                return RedirectToAction("Index", "ProductTypes");
            }

            foreach (var product in model.CartProducts)
            {
                if (product.IsActive)
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

        public async Task<IActionResult> CompleteOrder(OrderView orderView)
        {
            User user = ActiveUser.Instance.User;
            int? userId = user.UserId;
            if (userId == null)
            {
                return Redirect("ProductTypes");
            }

            Order openOrder = await(
                from order in context.Order
                where order.UserId == userId && order.DateCompleted == null
                select order).SingleOrDefaultAsync();

            if (openOrder == null)
            {
                return RedirectToAction("Buy", "ProductTypes");
            }

            //check for a selected PaymentId, otherwise redirect
            if (orderView.selectedPaymentId > 0)
            {
                openOrder.PaymentTypeId = orderView.selectedPaymentId;
            } else
            {
                return RedirectToAction("Final","Order");
            }

            try
            {
                openOrder.DateCompleted = DateTime.Now;
                context.Order.Update(openOrder);
                await context.SaveChangesAsync();
                return RedirectToAction("Confirmation", new RouteValueDictionary(
                     new { controller = "Cart", action = "Confirmation", Id = openOrder.OrderId } ));

            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
        public async Task<IActionResult> Confirmation(int id)
        {

            Order CompleteOrder = await(
                from order in context.Order
                where order.OrderId == id
                select order).SingleOrDefaultAsync();

            if (CompleteOrder == null)
            {
                return RedirectToAction("Buy", "ProductTypes");
            }

            if (CompleteOrder.UserId != CompleteOrder.UserId)
            {
                return Redirect("ProductTypes");
            }



            var LineItems = await(
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == id && lineItem.ProductId == product.ProductId && lineItem.Product.IsActive == true)
                select product).ToListAsync();


            var model = new CartView(context);

            //Mock information, will be removed once payment selector is avaliable
            if (CompleteOrder.PaymentType == null)
            {
                PaymentType Paypal = new PaymentType();
                Paypal.Description = "Paypal";
                CompleteOrder.PaymentType = Paypal;
            };
            model.PaymentType = CompleteOrder.PaymentType;
            model.LineItems = LineItems;
            model.Order = CompleteOrder;

            foreach (var product in LineItems)
            {
                if (product.IsActive)
                    model.TotalPrice += product.Price;
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
